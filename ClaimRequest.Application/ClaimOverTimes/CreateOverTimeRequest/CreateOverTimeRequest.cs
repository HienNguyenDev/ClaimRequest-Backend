using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.CreateOverTimeRequest;

public abstract class CreateOverTimeRequest
{
    public class Command : ICommand
    { 
        public Guid ProjectId { get; set; }
        public Guid ApproverId { get; set; }
        
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<DateOnly> OverTimeDates { get; set; }
        public List<Guid> OverTimeMembersIds { get; set; }
        public bool HasWeekend { get; set; }
        public bool HasWeekday { get; set; }
        public Guid RoomId { get; set; }
        
    }
    
    public class Handler
                    (IDbContext dbContext, 
                    IUserContext userContext) : ICommandHandler<Command>
    {
        public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
        {
            var isProjectExisted = await dbContext.Projects.AnyAsync(p => p.Id == command.ProjectId);

            if (!isProjectExisted)
            {
                return Result.Failure(ProjectErrors.NotFound(command.ProjectId));
            }
            
            var userId = userContext.UserId;
            var isProjectManger = await dbContext.ProjectMembers
                                                    .AnyAsync(p => p.ProjectID == command.ProjectId 
                                                                            && p.UserID == userId 
                                                                            && p.RoleInProject == RoleInProject.ProjectManager, cancellationToken);

            if (!isProjectManger)
            {
                return Result.Failure(ProjectmemberErrors.UserNotIsProjectManager(userId, command.ProjectId));
            }
            
            var user = await dbContext.Users
                .SingleAsync(u => u.Id == userId, cancellationToken);

            var isBuLeader = await dbContext.Users.AnyAsync(u =>
                u.Id == command.ApproverId &&
                u.Role == UserRole.BULeader &&
                u.DepartmentId == user.DepartmentId, cancellationToken);

            if (!isBuLeader)
            {
                return Result.Failure(OverTimeRequestErrors.InvalidApprover); 
            } 
            
            var memberExisted = await dbContext.Users
                .Where(u => command.OverTimeMembersIds.Contains(u.Id))
                .Select(u => u.Id).ToListAsync();
            
            var nonExistingUserIds = command.OverTimeMembersIds.Except(memberExisted).ToList();

            if (nonExistingUserIds.Any())
            {
                return Result.Failure(UserErrors.NotFound(nonExistingUserIds));
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            if (command.StartDate < today)
            {
                return Result.Failure(OverTimeRequestErrors.InvalidStartDate());
            }

            if (command.EndDate < command.StartDate)
            {
                return Result.Failure(OverTimeRequestErrors.InvalidEndDate());
            }
            
            if (!command.OverTimeDates.All(date => date >= command.StartDate && date <= command.EndDate))
            {
                return Result.Failure(OverTimeRequestErrors.InvalidOverTimeDates());
            }

            if (command.OverTimeDates.Distinct().Count() != command.OverTimeDates.Count)
            {
                return Result.Failure(OverTimeRequestErrors.DuplicateOverTimeDates());
            }

            var request = new OverTimeRequest
            {
                Id = Guid.NewGuid(),
                ProjectId = command.ProjectId,
                Status = OverTimeRequestStatus.Pending,
                ApproverId = command.ApproverId,
                ProjectManagerId = userId,
                CreatedAt = DateTime.UtcNow,
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                RoomId = command.RoomId,
                HasWeekend = command.HasWeekend,
                HasWeekday = command.HasWeekday,
            };

            var overTimeDates = command.OverTimeDates.Select(od => new OverTimeDate
            {
                Id = Guid.NewGuid(),
                OverTimeRequestId = request.Id,
                Date = od,
            });

            var overTimeMembers = command.OverTimeMembersIds.Select(om => new OverTimeMember
            {
                Id = Guid.NewGuid(),
                UserId = om,
                RequestId = request.Id,
            });
            
            var username = user.FullName;
            request.Raise(new OverTimeRequestCreatedEvent()
            {
                BuleadId = request.ApproverId,
                UserName = username,
            });
            dbContext.OverTimeRequests.Add(request);
            dbContext.OverTimeDates.AddRange(overTimeDates);
            dbContext.OverTimeMembers.AddRange(overTimeMembers);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(command => command.ApproverId).NotEmpty().NotNull();
            RuleFor(command => command.ProjectId).NotEmpty().NotNull();
            RuleFor(command => command.HasWeekday).NotNull();
            RuleFor(command => command.HasWeekend).NotNull();
            RuleFor(command => command.OverTimeMembersIds).NotEmpty().NotNull();
            RuleFor(command => command.StartDate).NotNull();
            RuleFor(command => command.EndDate).NotNull();
            RuleFor(command => command.OverTimeDates).NotEmpty().NotNull();
        }
    }
    
}