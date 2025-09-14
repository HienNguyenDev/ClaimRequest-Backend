using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Domain.ClaimOverTime.Events;

namespace ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeRequest
{
    public class ApproveOverTimeRequestCommandHandler(IDbContext context, IUserContext userContext) : ICommandHandler<ApproveOverTimeRequestCommand>
    {
        public async Task<Result> Handle(ApproveOverTimeRequestCommand command, CancellationToken cancellationToken)
        {
            var request = await context.OverTimeRequests
                .FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (request == null)
            {
                return Result.Failure<Guid>(OverTimeRequestErrors.NotFound(command.Id));
            }
            if (request.Status != OverTimeRequestStatus.Pending)
            {
                return Result.Failure<Guid>(OverTimeRequestErrors.InvalidStatus(request.Status, OverTimeRequestStatus.Pending));
            }

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (request.EndDate < today)
            {
                return Result.Failure<Guid>(OverTimeRequestErrors.ExpiredDate(request.EndDate));
            }

            var userId = userContext.UserId;
            var userMember = await context.OverTimeMembers.FirstOrDefaultAsync(u => u.UserId == userId);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId && u.IsSoftDelete == 0, cancellationToken);
            if (user == null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound(userId));
            }

            if (userId != request.ApproverId)
            {
                return Result.Failure<Guid>(OverTimeRequestErrors.ActionNotAllowedForThisUserRole(user.Role));
            }

            request.Status = OverTimeRequestStatus.Approved;
            request.Raise(new OverTimeRequestApprovedEvent{
                RequestId = request.Id,
                //ApproverName = userId
            });
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
