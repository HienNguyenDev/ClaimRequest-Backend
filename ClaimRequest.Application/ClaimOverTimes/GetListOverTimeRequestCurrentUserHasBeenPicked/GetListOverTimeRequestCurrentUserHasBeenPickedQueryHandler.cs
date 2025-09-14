using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
namespace ClaimRequest.Application.ClaimOverTimes.GetListOverTimeRequestCurrentUserHasBeenPicked
{
    public class GetListOverTimeRequestCurrentUserHasBeenPickedQueryHandler(IDbContext dbContext, IUserContext userContext) : IQueryHandler<GetListOverTimeRequestCurrentUserHasBeenPickedQuery, Page<GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse>>
    {
        public async Task<Result<Page<GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse>>> Handle(GetListOverTimeRequestCurrentUserHasBeenPickedQuery request, CancellationToken cancellationToken)
        {
            var user = userContext.UserId;
            
            var overTimeRequest =  dbContext.OverTimeRequests.Where(x=>x.OverTimeMembers.Any(x=>x.UserId == user) && x.Status == Domain.ClaimOverTime.OverTimeRequestStatus.Approved).OrderBy(x=>x.CreatedAt).Include(x=>x.OverTimeDates).Include(x=>x.Project).Include(x => x.OverTimeMembers).ThenInclude(x => x.User).Include(x=>x.Room).ThenInclude(x=>x.Site).Include(x=>x.CreatedByUser);
            
            var total = await overTimeRequest.CountAsync(cancellationToken);

            var result = await overTimeRequest.ApplyPagination(
                request.PageNumber,
                request.PageSize).Select(u => new GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse
                {
                    Id = u.Id,
                    ProjectId = u.ProjectId,
                    ProjectName = u.Project.Name,
                    ProjectManagerId = u.ProjectManagerId,
                    ProjectManagerName = u.CreatedByUser.FullName,
                    CreatedAt = u.CreatedAt,
                    Status = u.Status,
                    ApproverId = u.ApproverId,
                    ApproverName = u.Approver.FullName,
                    StartDate = u.StartDate,
                    EndDate = u.EndDate,
                    RoomId = u.RoomId,
                    RoomName = u.Room.Name,
                    SiteId = u.Room.SiteId,
                    SiteName = u.Room.Site.Name,
                    HasWeekday = u.HasWeekday,
                    HasWeekend = u.HasWeekend,
                    OverTimeMemBer = u.OverTimeMembers.Select(u=> new OverTimeMemBerResponse 
                    {
                        Id=u.Id,
                        UserId = u.UserId,
                        UserName = u.User.FullName,
                    }).ToList(),
                    OverTimeDate = u.OverTimeDates.Select(u=> new OverTimeDateResponse 
                    {
                        Id=u.Id,
                        Date = u.Date,
                        
                    }).ToList(),

                }).ToListAsync(cancellationToken);

            return new Page<GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse>
            (result,
                total,
                request.PageNumber,
                request.PageSize);
        }

      
    }
}

