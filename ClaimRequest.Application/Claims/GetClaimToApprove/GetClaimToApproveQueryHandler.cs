using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Claims.GetClaimToApprove
{
    public class GetClaimToApproveQueryHandler(IDbContext db, IUserContext userContext) : IQueryHandler<GetClaimToApproveQuery, Page<GetCLaimToApproveResponse>>
    {
     
        public async Task<Result<Page<GetCLaimToApproveResponse>>> Handle(GetClaimToApproveQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.UserId;   
            var list =  db.Claims.Where(u => u.Status == ClaimStatus.Confirmed && u.ApproverId == user  && u.IsSoftDeleted == 0)
                .Include(u=>u.Approver)
                .Include(u=>u.ClaimDetails)
                .Include(u => u.User)
                .Include(u=>u.Reason)
                .ThenInclude(u=>u.ReasonType)
                .Include(u=>u.Supervisor)
                .Include(u=>u.InformTos)
                .ThenInclude(u=>u.User);

            var total = await list.CountAsync(cancellationToken);
            var result = await list.ApplyPagination(
                request.PageNumber,
                request.PageSize).Select(u => new GetCLaimToApproveResponse
                {
                    Id = u.Id,
                    UserName = u.User.FullName,
                    ReasonId = u.ReasonId,
                    ReasonName = u.Reason.Name,
                    ReasonTypeId = u.Reason.RequestTypeId,
                    ReasonTypeName = u.Reason.ReasonType.Name,
                    OtherReasonText = u.OtherReasonText,
                    Status = u.Status,
                    SupervisorId = u.SupervisorId,
                    SupervisorFullName = u.Supervisor.FullName,
                    ApproverId = u.ApproverId,
                    ApproverFullName = u.Approver.FullName,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    Partial = u.Partial,
                    ExpectApproveDay = u.ExpectApproveDay,
                    ClaimFee = u.ClaimFee,
                    ClaimDates = u.ClaimDetails.Select(c => c.Date).ToArray(),
                    InformTos = u.InformTos.Select(x => new InformToResponse()
                    {
                        UserId = x.UserId,
                        UserName = x.User.FullName,
                    }).ToList(),


                }).ToListAsync(cancellationToken);
          
            return new Page<GetCLaimToApproveResponse>
            (result, 
                total, 
                request.PageNumber, 
                request.PageSize);
        }
    }
}
