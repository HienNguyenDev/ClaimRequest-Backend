using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.PaidOverTimeEffort;

public abstract class PaidOverTimeEffort
{
    public sealed class PaidOverTimeEffortCommand : ICommand
    {
        public Guid Id { get; set; }
    }
    
    public class PaidOverTimeEffortCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<PaidOverTimeEffortCommand>
    {
        public async Task<Result> Handle(PaidOverTimeEffortCommand command, CancellationToken cancellationToken)
        {
            var effort = await context.OverTimeEffort.Include(c => c.OverTimeMember)
                .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (effort == null)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.NotFound(command.Id));
            }
            if (effort.Status != OverTimeEffortStatus.Approved)
            {
                return Result.Failure<Guid>(OverTimeEffortErrors.InvalidStatus(effort.Status, OverTimeEffortStatus.Approved));
            }

            var userId = userContext.UserId;
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsSoftDelete == 0, cancellationToken);
            if (user == null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound(userId));
            }
            

            effort.Status = OverTimeEffortStatus.Paid;
            effort.Raise(new OverTimeEffortStatusChangedEvent()
            {
                EffortId = effort.Id,
                Action = "Paid",
                UserId = userId,
            });
           
            effort.Raise(new OverTimeEffortPaidEvent
            {
                UserId = effort.OverTimeMember.UserId,
                Effort = effort.DayHours + effort.NightHours,
            });
            
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
    
}