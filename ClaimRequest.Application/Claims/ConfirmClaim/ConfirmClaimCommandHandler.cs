using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims.Errors;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Claims.ConfirmClaim
{
    public class ConfirmClaimCommandHandler
            (IDbContext context,
            IUserContext userContext) : ICommandHandler<ConfirmClaimCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(ConfirmClaimCommand command, CancellationToken cancellationToken)
        {
            var claim = await context.Claims.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (claim == null)
            {
                return Result.Failure<Guid>(ClaimErrors.NotFound(command.Id));
            }

            if(claim.Status != Domain.Claims.ClaimStatus.Pending)
            {
                return Result.Failure<Guid>(ClaimErrors.InvalidStatus(claim.Status));
            }

            claim.Status = Domain.Claims.ClaimStatus.Confirmed;


            var userId = userContext.UserId;
            var claimOwnerUser = claim.UserId;
            var userName = context.Users
                .FirstOrDefault(u => u.Id == userId)?
                .FullName;
            claim.Raise(new ClaimConfirmedDomainEvent
            {
                ClaimId = claim.Id,
                Action = "Confirm",
                UserId = userId,
                PerformedAt = DateTime.UtcNow
            });

            claim.Raise(new ClaimStatusChangedDomainEvent
            {
                UserId = claimOwnerUser,
                ClaimId = claim.Id,
                Action = "Confirmed",
                UserActionName = userName ?? "System",
            });
            
            
            await context.SaveChangesAsync(cancellationToken);

            return claim.Id;
        }
    }
}
