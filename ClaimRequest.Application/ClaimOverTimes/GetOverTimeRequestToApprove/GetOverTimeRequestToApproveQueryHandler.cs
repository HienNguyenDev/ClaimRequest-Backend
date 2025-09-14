using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeRequestToApprove;

public class GetOverTimeRequestToApproveQueryHandler(IDbContext db, IUserContext userContext)
    : IQueryHandler<GetOverTimeRequestToApproveQuery, Page<GetOverTimeRequestToApproveResponse>>
{
    public async Task<Result<Page<GetOverTimeRequestToApproveResponse>>> Handle(GetOverTimeRequestToApproveQuery requestToApprove,
        CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        var list = db.OverTimeRequests.Where(o => o.ApproverId == userId && o.Status == OverTimeRequestStatus.Pending)
            .Include(o => o.Approver)
            .Include(o => o.Project)
            .Include(o => o.CreatedByUser)
            .Include(o => o.Room).ThenInclude(r => r.Site)
            .Include(o => o.OverTimeDates)
            .Include(o => o.OverTimeMembers).ThenInclude(m => m.User);


        var total = await list.CountAsync(cancellationToken);
        var result = await list.OrderByDescending(o => o.CreatedAt)
            .ApplyPagination(requestToApprove.PageNumber, requestToApprove.PageSize).Select(o => new GetOverTimeRequestToApproveResponse
            {
                Id = o.Id,
                ProjectId = o.ProjectId,
                ProjectName = o.Project.Name,
                ProjectManagerId = o.ProjectManagerId,
                CreatedAt = o.CreatedAt,
                CreatedByUser = o.CreatedByUser.FullName,
                StartDate = o.StartDate,
                EndDate = o.EndDate,
                Status = o.Status,
                ApproverId = o.ApproverId,
                ApproverFullName = o.Approver.FullName,
                RoomId = o.RoomId,
                RoomName = o.Room.Name,
                SiteId = o.Room.SiteId,
                SiteName = o.Room.Site.Name,
                HasWeekday = o.HasWeekday,
                HasWeekend = o.HasWeekend,
                OverTimeMemBer = o.OverTimeMembers.Select(u=> new OverTimeMemBerResponse 
                {
                    Id=u.Id,
                    UserId = u.UserId,
                    UserName = u.User.FullName,
                }).ToList(),
                OverTimeDate = o.OverTimeDates.Select(u=> new OverTimeDateResponse 
                {
                    Id=u.Id,
                    Date = u.Date,
                }).ToList(),

            }).ToListAsync(cancellationToken);
        return new Page<GetOverTimeRequestToApproveResponse>
            (result, total, requestToApprove.PageNumber, requestToApprove.PageSize);
    }
}