using System.Runtime.InteropServices.JavaScript;
using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Claims.Errors;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.LateEarlyCases;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Users;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Claims.CreateClaim;

public class CreateClaimCommandHandler
                    (IDbContext dbContext, 
                    IUserContext userContext) : ICommandHandler<CreateClaimCommand> 
{
    public async Task<Result> Handle(CreateClaimCommand command, CancellationToken cancellationToken)
    {
     
        Guid userId = userContext.UserId;
        var user = await dbContext.Users
                            .Include(u => u.Projects)
                            .SingleAsync(u => u.Id == userId, cancellationToken);

        var buLeader = await dbContext.Users.SingleOrDefaultAsync(u =>
            u.Id == command.ApproverId &&
            u.Role == UserRole.BULeader &&
            u.DepartmentId == user.DepartmentId, cancellationToken);

        if (buLeader == null) 
        {
            return Result.Failure(ClaimErrors.InvalidApprover); 
        }

        if (buLeader.Id != command.SupervisorId)
        {
            var projects = user.Projects
                .Where(p => p.Members
                    .Any(m => m.RoleInProject != RoleInProject.ProjectManager && 
                              m.UserID == userId))
                .Select(p => p.Id)
                .ToList();

            var isSupervisor = dbContext.ProjectMembers
                .Any(p => projects.Contains(p.ProjectID) && 
                          p.RoleInProject == RoleInProject.ProjectManager && 
                          p.UserID == command.SupervisorId);
            
            if (!isSupervisor)
            {
                return Result.Failure(ClaimErrors.InvalidSupervisor);
            }
        }
        
        Claim claim = new Claim
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            StartDate = command.StartDate,
            EndDate = command.EndDate,
            ApproverId = command.ApproverId,
            SupervisorId = command.SupervisorId, 
            Partial = command.Partial,
            OtherReasonText = command.OtherReasonText,
            ReasonId = command.ReasonId,
            Status = ClaimStatus.Pending,
            ExpectApproveDay = command.ExpectApproveDay,
            ClaimFee = command.ClaimFee,
            CreatedAt = DateTime.UtcNow,
        };
        
        DateOnly now = DateOnly.FromDateTime(DateTime.UtcNow);

        foreach (var date in command.DatesForClaim)
        {
            
            if (date < now)
            {
                /*DateTime firstDayOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
                DateTime cutoffDate = new DateTime(firstDayOfNextMonth.Year, firstDayOfNextMonth.Month, 5);*/
                DateOnly firstDayOfNextMonth = new DateOnly(date.Year, date.Month, 1).AddMonths(1);
                DateOnly cutoffDate = new DateOnly(firstDayOfNextMonth.Year, firstDayOfNextMonth.Month, 3);
                
                if (now > cutoffDate)
                {
                    return Result.Failure(ClaimErrors.AddClaimToLate);
                }

                LateEarlyCase? lateEarlyCase = null;
                AbnormalCase? abnormalCase = null;
                
                if (command.DatesForClaim.Count == 1)
                {
                    
                    if (command.Partial == Partial.Morning)
                    {
                        lateEarlyCase = await dbContext.LateEarlyCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == userId && a.IsLateCome, cancellationToken);
                
                        abnormalCase = await dbContext.AbnormalCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == userId && a.AbnormalType == AbnormalType.OffMorningWithoutPermission, cancellationToken);
                        
                    }
                
                    if (command.Partial == Partial.Afternoon)
                    {
                        lateEarlyCase = await dbContext.LateEarlyCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == userId && a.IsEarlyLeave, cancellationToken);
                
                        abnormalCase = await dbContext.AbnormalCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == userId && a.AbnormalType == AbnormalType.OffAfternoonWithoutPermission, cancellationToken);
                    }
                
                    if (command.Partial == Partial.AllDay)
                    {
                        lateEarlyCase = await dbContext.LateEarlyCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == userId && a.IsEarlyLeave && a.IsLateCome, cancellationToken);
                        
                        abnormalCase = await dbContext.AbnormalCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == userId && a.AbnormalType == AbnormalType.OffAllDayWithoutPermission, cancellationToken);
                    }
                }

                if (lateEarlyCase == null && abnormalCase == null)
                {
                    return Result.Failure(ClaimErrors.NoAbnormalCaseOrLateEarlyCaseForThisDay());
                }
                
                if (lateEarlyCase != null)
                {
                    var check = dbContext.ClaimDetails.Where(cd => cd.LateEarlyId == lateEarlyCase.Id).Any(cd => cd.Claim.Status != ClaimStatus.Cancel && cd.Claim.Status != ClaimStatus.Rejected);
                    if (check)
                    {
                        return Result.Failure(ClaimErrors.ClaimExistedForThisLateEarlyCase());
                    }
                    
                }
                
                if (abnormalCase != null)
                {
                    var check = dbContext.ClaimDetails.Where(cd => cd.AbnormalId == abnormalCase.Id).Any(cd => cd.Claim.Status != ClaimStatus.Cancel && cd.Claim.Status != ClaimStatus.Rejected);
                    if (check)
                    {
                        return Result.Failure(ClaimErrors.ClaimExistedForThisAbnormalCase());
                    }
                }
                
                
                ClaimDetail detail = new ClaimDetail
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    ClaimId = claim.Id,
                    AbnormalId = abnormalCase?.Id,
                    LateEarlyId = lateEarlyCase?.Id
                };
                claim.ClaimDetails.Add(detail);
            }
            else
            {
                ClaimDetail detail = new ClaimDetail
                {
                    Id = Guid.NewGuid(),
                    Date = date,
                    ClaimId = claim.Id,
                };
                claim.ClaimDetails.Add(detail);
            }
        }

        foreach (var informedUserId in command.InformTos)
        {
            claim.InformTos.Add(new InformTo
            {
                ClaimId = claim.Id,
                UserId = informedUserId,
            });
        }

        var userName = dbContext.Users
                .FirstOrDefault(u => u.Id == userId)?
                .FullName;


        claim.Raise(new ClaimCreatedDomainEvent(command.SupervisorId)
        {
            ClaimId = claim.Id,
            UserName = userName,
            InformTo = claim.InformTos.Select(i => i.UserId).ToList(),
        });
        
        dbContext.Claims.Add(claim);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(); 
    }
}