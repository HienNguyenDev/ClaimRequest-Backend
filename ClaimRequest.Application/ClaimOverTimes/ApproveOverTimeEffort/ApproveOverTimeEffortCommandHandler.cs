using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Domain.ClaimOverTime.Events;

namespace ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeEffort
{
    public class ApproveOverTimeEffortCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<ApproveOverTimeEffortCommand>
    {
        public async Task<Result> Handle(ApproveOverTimeEffortCommand command, CancellationToken cancellationToken)
        {
            var effort = await context.OverTimeEffort.Include(c => c.OverTimeMember)
                                                     .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (effort == null)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.NotFound(command.Id));
            }
            if (effort.Status != OverTimeEffortStatus.Confirmed)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.InvalidStatus(effort.Status, OverTimeEffortStatus.Confirmed));
            }

            var userId = userContext.UserId;
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsSoftDelete == 0, cancellationToken);
            if (user == null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound(userId));
            }

            var otRequest = await context.OverTimeRequests.FirstOrDefaultAsync(o => o.Id == effort.OverTimeMember.RequestId, cancellationToken);
            if (otRequest == null)
            {
                return Result.Failure<Guid>(OverTimeRequestErrors.NotFound(effort.OverTimeMember.RequestId));
            }
            if (userId != otRequest.ApproverId)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.ActionNotAllowedForThisUserRole(user.Role));
            }

            effort.Status = OverTimeEffortStatus.Approved;
            
            effort.Raise(new OverTimeEffortStatusChangedEvent
            {
                EffortId = effort.Id,
                Action = "Approved",
                UserId = userId,
            });
            
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
