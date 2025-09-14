using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Claims.GetClaimToConfirm
{
    public class GetClaimToConfirmQueryHandler(IDbContext dbContext, IUserContext userContext) : IQueryHandler<GetClaimToConfirmQuery, Page<GetClaimToConfirmQueryResponse>>
    {
        public async Task<Result<Page<GetClaimToConfirmQueryResponse>>> Handle(GetClaimToConfirmQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.UserId;
            var claim = dbContext.Claims
                .Where(u=>u.Status == ClaimStatus.Pending && u.SupervisorId == user && u.IsSoftDeleted == 0)
                .Include(u => u.Approver)
                .Include(u => u.ClaimDetails)
                .Include(u=>u.User)
                .Include(u=>u.Reason)
                .ThenInclude(u=>u.ReasonType)
                .Include(u => u.Supervisor)
                .Include(u=>u.InformTos)
                ;
            int totalCount = await claim.CountAsync(cancellationToken);
            var response = await claim.ApplyPagination(request.PageNumber,request.PageSize).Select(u=>new GetClaimToConfirmQueryResponse
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
                ClaimDates = u.ClaimDetails.Select(u=>u.Date).ToArray(),
                InformTos = u.InformTos.Select(x=>  new InformToResponse() 
                {
                    UserId = x.UserId,
                    UserName = x.User.FullName,
                }).ToList()
                
            }).ToListAsync(cancellationToken);
            return new Page<GetClaimToConfirmQueryResponse>(response, totalCount, request.PageNumber, request.PageSize);

        }
    }
}