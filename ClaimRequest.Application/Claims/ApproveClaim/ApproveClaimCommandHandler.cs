using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims.Errors;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.LateEarlyCases;

namespace ClaimRequest.Application.Claims.ApproveClaim
{
    public class ApproveClaimCommandHandler(IDbContext dbContext, IUserContext userContext) : ICommandHandler<ApproveClaimCommand, string>
    {
        public async Task<Result<string>> Handle(ApproveClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await dbContext.Claims.Include(c => c.ClaimDetails).FirstOrDefaultAsync(u => u.Id == request.Id , cancellationToken);
            if (claim == null) 
            {
                return Result.Failure<string>(ClaimErrors.NotFound(request.Id));
            }


            if(claim.Status != ClaimStatus.Confirmed) 
            {
                return Result.Failure<string>(ClaimErrors.InValidStatusForApproveClaim(claim.Status));
            }

            var claimOwnerUser = claim.UserId;
            var userId = userContext.UserId;
            var userName = dbContext.Users
                .FirstOrDefault(u => u.Id == userId)?
                .FullName;
            
            LateEarlyCase? lateEarlyCase = null;
            AbnormalCase? abnormalCase = null;
            
            if (claim.ClaimDetails.Count == 1)
            {
                var detail = claim.ClaimDetails.First();
                var date = detail.Date;

                if (detail.LateEarlyId == null && detail.AbnormalId == null)
                {
                    if (claim.Partial == Partial.Morning)
                    {
                        lateEarlyCase = await dbContext.LateEarlyCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == claim.UserId && a.IsLateCome, cancellationToken);
                
                        abnormalCase = await dbContext.AbnormalCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == claim.UserId && a.AbnormalType == AbnormalType.OffMorningWithoutPermission, cancellationToken);
                        
                    }
                
                    if (claim.Partial == Partial.Afternoon)
                    {
                        lateEarlyCase = await dbContext.LateEarlyCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == claim.UserId && a.IsEarlyLeave, cancellationToken);
                
                        abnormalCase = await dbContext.AbnormalCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == claim.UserId && a.AbnormalType == AbnormalType.OffAfternoonWithoutPermission, cancellationToken);
                    }
                
                    if (claim.Partial == Partial.AllDay)
                    {
                        lateEarlyCase = await dbContext.LateEarlyCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == claim.UserId && a.IsEarlyLeave && a.IsLateCome, cancellationToken);
                        
                        abnormalCase = await dbContext.AbnormalCases
                            .SingleOrDefaultAsync(a => a.WorkDate == date && a.UserId == claim.UserId && a.AbnormalType == AbnormalType.OffAllDayWithoutPermission, cancellationToken);
                    }
                    
                    if (lateEarlyCase != null)
                    {
                        claim.ClaimDetails.First().LateEarlyId = lateEarlyCase.Id;
                    }

                    if (abnormalCase != null)
                    {
                        claim.ClaimDetails.First().AbnormalId = abnormalCase.Id;
                    }
                }
            }
            
            
            claim.Status = ClaimStatus.Approved;
            dbContext.Claims.Update(claim);
            
            claim.Raise(new ClaimApprovedDomainEvent()
            {
                ClaimId = claim.Id,
                Action = "Approved",
                PerformedAt = DateTime.UtcNow,
                UserId = userId
            });

            claim.Raise(new ClaimStatusChangedDomainEvent
            {
                UserId = claimOwnerUser,
                ClaimId = claim.Id,
                Action = "Approved",
                UserActionName = userName ?? "System",
            });

            
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success($"Claim with {claim.Id} is approved");
        }
    }
}
