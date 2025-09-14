using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.CreateOverTimeEffort
{
    public class CreateOverTimeEffortCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<CreateOverTimeEffortCommand>
    {
        public async Task<Result> Handle(CreateOverTimeEffortCommand command, CancellationToken cancellationToken)
        {
            var request = await context.OverTimeRequests
                .Include(or => or.CreatedByUser)
                .FirstOrDefaultAsync(r => r.Id == command.RequestId);
            
            if (request == null)
            {
                return Result.Failure(OverTimeRequestErrors.NotFound(command.RequestId));
            }

            var userId = userContext.UserId;
            var member = await context.OverTimeMembers.FirstOrDefaultAsync(m => m.Id == command.OverTimeMemberId 
                                                                       && m.UserId == userId 
                                                                       && m.RequestId == command.RequestId);
            if (member == null)
            {
                return Result.Failure(OverTimeMemberErrors.NotFound(command.RequestId));
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var date = await context.OverTimeDates.FirstOrDefaultAsync(d => d.Id == command.OverTimeDateId
                                                                   && d.Date < today
                                                                   && d.OverTimeRequestId == command.RequestId);
            if(date == null)
            {
                return Result.Failure(OverTimeDateErrors.NotFound(command.OverTimeDateId));
            }

            var effortExisted = await context.OverTimeEffort.FirstOrDefaultAsync(e => e.OverTimeMemberId == command.OverTimeMemberId 
                                                                                   && e.OverTimeDateId == command.OverTimeDateId);
            if(effortExisted != null)
            {
                return Result.Failure(OverTimeEffortErrors.EffortExisted(command.RequestId, command.OverTimeMemberId, command.OverTimeDateId));
            }

            OverTimeEffortStatus status = OverTimeEffortStatus.Pending;
            if(userId == request.ProjectManagerId)
            {
                status = OverTimeEffortStatus.Confirmed;
            }

            OverTimeEffort effort = new OverTimeEffort
            {
                Id = Guid.NewGuid(),
                OverTimeMemberId = command.OverTimeMemberId,
                OverTimeDateId = command.OverTimeDateId,
                DayHours = command.DayHours,
                NightHours = command.NightHours,
                TaskDescription = command.TaskDescription,
                Status = status
            };
            
            effort.Raise(new OverTimeEffortCreatedEvent
            {
                SupervisorId = request.ProjectManagerId,
                UserId = userId,
                ApproverId = request.ApproverId
            });

            context.OverTimeEffort.Add(effort);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
