using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Users.Errors;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.RejectOverTimeRequest
{
    public class RejectOverTimeRequestCommandHandler(IDbContext dbContext, IUserContext userContext) : ICommandHandler<RejectOverTimeRequestCommand, string>
    {
        public async Task<Result<string>> Handle(RejectOverTimeRequestCommand request, CancellationToken cancellationToken)
        {
            var UserId = userContext.UserId;
            var User = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == UserId, cancellationToken);
            if (User == null) {
                return Result.Failure<string>(UserErrors.NotFound(UserId));
            }

            var claimOT = await dbContext.OverTimeRequests.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (claimOT == null)
            {
                return Result.Failure<string>(OverTimeRequestErrors.NotFound(request.Id));
            }
            if (claimOT.Status == Domain.ClaimOverTime.OverTimeRequestStatus.Rejected)
            {
                return Result.Failure<string>(OverTimeRequestErrors.Rejected(request.Id));
            }
            if(claimOT.ApproverId != UserId)
            {
                return Result.Failure<string>(OverTimeRequestErrors.ActionNotAllowedForThisUserRole(User.Role));
            }
            claimOT.Status = Domain.ClaimOverTime.OverTimeRequestStatus.Rejected;
            claimOT.Raise(new OverTimeRequestRejectedEvent
            {
                RequestId = claimOT.Id
            });
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success($"OverTime Request {request.Id} has been rejected");
        }
    }
}
