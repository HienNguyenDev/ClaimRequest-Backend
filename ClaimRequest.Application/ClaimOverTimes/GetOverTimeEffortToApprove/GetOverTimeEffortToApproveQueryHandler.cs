using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;

public class GetOverTimeEffortToApproveQueryHandler(IDbContext context,IUserContext userContext)
    : IQueryHandler<GetOverTimeEffortToApproveQuery, Page<GetOverTimeEffortToApproveResponse>>
{
    public async Task<Result<Page<GetOverTimeEffortToApproveResponse>>> Handle(GetOverTimeEffortToApproveQuery request,
        CancellationToken cancellationToken)
    {
        var user = userContext.UserId;

        var list = context.OverTimeEffort.Where(e => e.Status == OverTimeEffortStatus.Confirmed && e.OverTimeMember.Request.ApproverId == user)
            .Include(e => e.OverTimeDate)
            .Include(e => e.OverTimeMember).ThenInclude(o => o.User)
            .Include(e => e.OverTimeMember.Request); 
        
        var total = await list.CountAsync(cancellationToken);
        var result = await list.ApplyPagination(
            request.PageNumber,
            request.PageSize).Select(e => new GetOverTimeEffortToApproveResponse
        {
            Id = e.Id,
            OverTimeMemberId = e.OverTimeMember.Id,
            OverTimeDateId = e.OverTimeDate.Id,
            DayHours = e.DayHours,
            NightHours = e.NightHours,
            TaskDescription = e.TaskDescription,
            Status = e.Status,
            OverTimeMember = new OverTimeMemberResponse()
            {
                Id = e.OverTimeMember.Id,
                UserId = e.OverTimeMember.UserId,
                RequestId = e.OverTimeMember.RequestId,
                UserName = e.OverTimeMember.User.FullName 
               
            },
            
            OverTimeDate = new OverTimeDateResponse()
            {
                Id = e.OverTimeDate.Id,
                Date = e.OverTimeDate.Date,
                OverTimeRequestId = e.OverTimeDate.OverTimeRequestId
            },
            
            OverTimeRequest = new OverTimeRequestResponse
            {
                Id = e.OverTimeMember.Request.Id,
                StartDate = e.OverTimeMember.Request.StartDate,
                EndDate = e.OverTimeMember.Request.EndDate,
                ProjectId = e.OverTimeMember.Request.ProjectId,
                ProjectName = e.OverTimeMember.Request.Project.Name,
                CreatedAt = e.OverTimeMember.Request.CreatedAt,
                Status = e.OverTimeMember.Request.Status
            }
        }).ToListAsync(cancellationToken);
        return new Page<GetOverTimeEffortToApproveResponse>(result, total, request.PageNumber, request.PageSize);

    }
}