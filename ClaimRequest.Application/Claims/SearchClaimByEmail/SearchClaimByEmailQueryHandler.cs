using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Application.Claims.SearchClaimByEmail;
using ClaimRequest.Domain.Claims.Errors;

namespace ClaimRequest.Application.Claims.SearchClaimByEmail
{
    public class SearchClaimByEmailQueryHandler(IDbContext db, IUserContext userContext) : IQueryHandler<SearchClaimByEmailQuery, Page<SearchClaimByEmailResponse>>
    {
        public  async Task<Result<Page<SearchClaimByEmailResponse>>> Handle(SearchClaimByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.UserId;
            var list = db.Claims.Where(u => u.User.Email == request.Email && u.IsSoftDeleted == 0)
                .Include(u => u.Approver)
                .Include(u => u.ClaimDetails)
                .Include(u => u.User)
                .Include(u => u.Reason)
                .ThenInclude(u => u.ReasonType)
                .Include(u => u.Supervisor)
                .Include(u => u.InformTos);
            var total = await list.CountAsync(cancellationToken);
            var result = await list.ApplyPagination(
                request.PageNumber,
                request.PageSize).Select(u => new SearchClaimByEmailResponse
                {
                    Id = u.Id,
                    UserId = u.UserId,
                    ReasonId = u.ReasonId,
                    OtherReasonText = u.OtherReasonText,
                    Status = u.Status,
                    IsSoftDeleted = u.IsSoftDeleted,
                    SupervisorId = u.SupervisorId,
                    ApproverId = u.ApproverId,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    Partial = u.Partial
                })
                .ToListAsync(cancellationToken);
            return new Page<SearchClaimByEmailResponse>
                (result,
                total,
                request.PageNumber,
                request.PageSize);
        }
    }
}
