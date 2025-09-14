using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.ConfirmOverTimeEffort
{
    public class ConfirmOverTimeEffortCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<ConfirmOverTimeEffortCommand>
    {
        public async Task<Result> Handle(ConfirmOverTimeEffortCommand command, CancellationToken cancellationToken)
        {
            var effort = await context.OverTimeEffort.Include(c => c.OverTimeMember)
                                                     .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (effort == null)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.NotFound(command.Id));
            }
            if (effort.Status != OverTimeEffortStatus.Pending)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.InvalidStatus(effort.Status, OverTimeEffortStatus.Pending));
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
            if (userId != otRequest.ProjectManagerId)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.ActionNotAllowedForThisUserRole(user.Role));
            }

            effort.Status = OverTimeEffortStatus.Confirmed;
            effort.Raise(new OverTimeEffortStatusChangedEvent
            {
                EffortId = effort.Id,
                Action = "Confirmed",
                UserId = userId,
            });
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
