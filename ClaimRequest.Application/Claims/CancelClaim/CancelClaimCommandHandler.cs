using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims.Errors;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Domain.Claims.Events;

namespace ClaimRequest.Application.Claims.CancelClaim
{
    public class CancelClaimCommandHandler : ICommandHandler<CancelClaimCommand>
    {
        private readonly IDbContext _context;
        private readonly IUserContext _userContext;
        public CancelClaimCommandHandler(IDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        public async Task<Result> Handle(CancelClaimCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var claim = await _context.Claims.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (claim == null)
                {
                    return Result.Failure(ClaimErrors.NotFound(request.Id));
                }
                //if (claim.Status == ClaimStatus.Returned || claim.Status == ClaimStatus.Confirmed || claim.Status == ClaimStatus.Approved || claim.Status == ClaimStatus.Pending)
                //{
                //    claim.Status = ClaimStatus.Cancel;
                //}
                if (claim.Status == ClaimStatus.Cancel || claim.Status == ClaimStatus.Draft)
                {
                    return Result.Failure(ClaimErrors.InValidStatusForCancelClaim(claim.Status));
                }
                claim.Status = ClaimStatus.Cancel;
                var userId = _userContext.UserId;
                claim.Raise(new ClaimConfirmedDomainEvent
                {
                    ClaimId = claim.Id,
                    Action = "Cancel",
                    UserId = userId,
                    PerformedAt = DateTime.UtcNow
                });

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(claim);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
