using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Application.Extensions;

namespace ClaimRequest.Application.Claims.GetPersonalByStatusClaim
{
    public class GetPersonalByStatusClaimQueryHandler(IDbContext context, IUserContext userContext) : IQueryHandler<GetPersonalByStatusClaimQuery, Page<GetPersonalByStatusClaimQueryResponse>>
    {
        public async Task<Result<Page<GetPersonalByStatusClaimQueryResponse>>> Handle(GetPersonalByStatusClaimQuery request, CancellationToken cancellationToken)
        {
            var userId = userContext.UserId;
            var list = context.Claims.Where(c => c.UserId == userId && c.Status == request.Status && c.IsSoftDeleted == 0)
                .Include(c => c.ClaimDetails)
                .Include(c => c.Supervisor)
                .Include(c => c.Approver)
                .Include(c => c.Reason)
                .Include(c=>c.User)
                .Include(c => c.InformTos).ThenInclude(i => i.User)
            ;
            int totalCount = await list.CountAsync(cancellationToken);
            var result = await list.ApplyPagination(
                request.PageNumber,
                request.PageSize).Select(c => new GetPersonalByStatusClaimQueryResponse
                {
                    Id = c.Id,
                    UserName = c.User.FullName,
                    ReasonId = c.ReasonId,
                    ReasonTypeId = c.Reason.RequestTypeId,
                    ReasonName = c.Reason.Name,
                    ReasonTypeName = c.Reason.ReasonType.Name,
                    OtherReasonText = c.OtherReasonText,
                    Status = c.Status,
                    SupervisorId = c.SupervisorId,
                    SupervisorFullName = c.Supervisor.FullName,
                    ApproverId = c.ApproverId,
                    ApproverFullName = c.Approver.FullName,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Partial = c.Partial,
                    ExpectApproveDay = c.ExpectApproveDay,
                    ClaimFee = c.ClaimFee,
                    ClaimDates = c.ClaimDetails.Select(d => d.Date).ToList(),
                    InformTos = c.InformTos.Select(i => new InformToResponse
                    {
                        UserId = i.UserId,
                        UserName = i.User.FullName
                    }).ToList()
                })
            .ToListAsync(cancellationToken);
            return new Page<GetPersonalByStatusClaimQueryResponse>(result, totalCount, request.PageNumber, request.PageSize);
        }

    }
}
