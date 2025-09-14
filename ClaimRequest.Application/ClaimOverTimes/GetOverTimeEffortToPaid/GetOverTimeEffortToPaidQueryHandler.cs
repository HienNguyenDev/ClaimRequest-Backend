using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToPaid;

public class GetOverTimeEffortToPaidQueryHandler(IDbContext context,IUserContext userContext)
    : IQueryHandler<GetOverTimeEffortToPaidQuery, Page<GetOverTimeEffortToPaidResponse>>
{
    public async Task<Result<Page<GetOverTimeEffortToPaidResponse>>> Handle(GetOverTimeEffortToPaidQuery request,
        CancellationToken cancellationToken)
    {
        var list = context.OverTimeEffort.Where(e => e.Status == OverTimeEffortStatus.Approved)
            .Include(e => e.OverTimeDate)
            .Include(e => e.OverTimeMember).ThenInclude(o => o.User)
            .Include(e => e.OverTimeMember.Request); 
        
        var total = await list.CountAsync(cancellationToken);
        var result = await list.ApplyPagination(
            request.PageNumber,
            request.PageSize).Select(e => new GetOverTimeEffortToPaidResponse
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
        return new Page<GetOverTimeEffortToPaidResponse>(result, total, request.PageNumber, request.PageSize);

    }
}