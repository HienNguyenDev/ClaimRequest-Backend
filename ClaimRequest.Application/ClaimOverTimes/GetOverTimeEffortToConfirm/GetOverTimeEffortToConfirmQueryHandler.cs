using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToConfirm;

public class GetOverTimeEffortToConfirmQueryHandler(IDbContext dbContext, IUserContext userContext) : IQueryHandler<GetOverTimeEffortToComfirmQuery,Page<GetOverTimeEffortToConfirmResponse>>
{
    public async Task<Result<Page<GetOverTimeEffortToConfirmResponse>>> Handle(GetOverTimeEffortToComfirmQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.UserId;
        var ot =  dbContext.OverTimeEffort.Where(
            x => x.OverTimeMember.Request.ProjectManagerId == user && x.Status == OverTimeEffortStatus.Pending
            ).Include(x => x.OverTimeMember).ThenInclude(x=>x.User).Include(x=>x.OverTimeDate);
       var total = await ot.CountAsync(cancellationToken);  
       var result = await ot.ApplyPagination(request.PageNumber, request.PageSize).Select(e=>new GetOverTimeEffortToConfirmResponse()
       {
           Id = e.Id,
           OverTimeMemberId = e.OverTimeMember.Id,
           OverTimeMemberName = e.OverTimeMember.User.FullName,
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
       return new Page<GetOverTimeEffortToConfirmResponse>(result, total, request.PageNumber, request.PageSize);
       
    }
}