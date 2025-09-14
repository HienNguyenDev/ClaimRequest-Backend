using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetListOvertimeRequestCurrentUserCreated;

public class GetListOvertimeRequestCurrentUserCreatedQueryHandler(IDbContext context, IUserContext userContext)
    : IQueryHandler<GetListOvertimeRequestCurrentUserCreatedQuery,
        Page<GetListOvertimeRequestCurrentUserCreatedQueryResponse>>
{
    public async Task<Result<Page<GetListOvertimeRequestCurrentUserCreatedQueryResponse>>> Handle(
        GetListOvertimeRequestCurrentUserCreatedQuery request,
        CancellationToken cancellationToken)
    {
        var requestCreatorId = userContext.UserId;
        var projectManager = context.Users
            .FirstOrDefault(u => u.Id == requestCreatorId);
        // var overTimeRequests =
        //     context.OverTimeRequests.Where(r => r.ProjectManagerId == requestCreatorId && r.Status == request.Status);
        var overTimeRequests = context.OverTimeRequests
            .Where(r => r.ProjectManagerId == requestCreatorId && r.Status == request.Status)
            .Include(o => o.Project)
            .Include(o => o.Approver)
            .Include(o => o.Room)
            .Include(o => o.Room.Site)
            .OrderBy(o => o.CreatedAt);
        if(projectManager == null)
        {
            return Result.Failure<Page<GetListOvertimeRequestCurrentUserCreatedQueryResponse>>(UserErrors.NotFound(projectManager.Id));
        }
        var result = await overTimeRequests.ApplyPagination(request.PageNumber, request.PageSize)
            .Select(o => new GetListOvertimeRequestCurrentUserCreatedQueryResponse
            {
                OverTimeRequestId = o.Id,
                ProjectManagerName = projectManager.FullName,
                ProjectName = o.Project != null ? o.Project.Name : "Unknown",
                CreatedDate = o.CreatedAt,
                Status = o.Status,
                ApproverName = o.Approver != null ? o.Approver.FullName : "Unknown",
                StartDate = o.StartDate,
                EndDate = o.EndDate,
                RoomName = o.Room != null ? o.Room.Name : "Unknown",
                HasWeekday = o.HasWeekday,
                HasWeekend = o.HasWeekend,
                SiteName = o.Room.Site != null ? o.Room.Site.Name : "Unknown",
            })
            .ToListAsync(cancellationToken);
        var totalCount = await overTimeRequests.CountAsync(cancellationToken);

        return new Page<GetListOvertimeRequestCurrentUserCreatedQueryResponse>(result, totalCount, request.PageNumber,
            request.PageSize);
    }
}