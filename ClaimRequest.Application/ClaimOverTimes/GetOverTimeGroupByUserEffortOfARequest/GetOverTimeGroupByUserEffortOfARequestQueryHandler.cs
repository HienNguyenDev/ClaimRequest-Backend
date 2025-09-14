using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeGroupByUserEffortOfARequest
{
    public class GetOverTimeGroupByUserEffortOfARequestQueryHandler(IDbContext context, IUserContext userContext) : IQueryHandler<GetOverTimeGroupByUserEffortOfARequestQuery, Page<GetOverTimeGroupByUserEffortOfARequestQueryResponse>>
    {
        public async Task<Result<Page<GetOverTimeGroupByUserEffortOfARequestQueryResponse>>> Handle(GetOverTimeGroupByUserEffortOfARequestQuery request, CancellationToken cancellationToken)
        {
            var MemberList = context.OverTimeMembers.Where(o => o.RequestId == request.Id)
                                                    .Include(o => o.User)
                                                    .Include(o => o.OverTimeEfforts);

            var EffortList = await context.OverTimeEffort.Where(o => o.OverTimeMember.RequestId == request.Id)
                                                         .Include(o => o.OverTimeMember)
                                                         .Include(o => o.OverTimeDate)
                                                         .ToListAsync(cancellationToken);

            var groupedEfforts = EffortList.GroupBy(e => e.OverTimeMemberId)
                                           .ToDictionary(g => g.Key, g => g.Select(e => new EffortOfAMemberInARequestResponse
                                           {
                                               OverTimeEffortId = e.Id,
                                               Date = e.OverTimeDate.Date,
                                               DayHours = e.DayHours,
                                               NightHours = e.NightHours,
                                               TaskDescription = e.TaskDescription,
                                               Status = e.Status
                                           }).ToList());

            int totalCount = await MemberList.CountAsync(cancellationToken);
            var result = await MemberList.ApplyPagination(
                request.PageNumber,
                request.PageSize).Select(o => new GetOverTimeGroupByUserEffortOfARequestQueryResponse
                {
                    OverTimeMemberId = o.Id,
                    UserId = o.UserId,
                    UserName = o.User.FullName,
                    MemberEfforts = groupedEfforts.ContainsKey(o.Id) ? groupedEfforts[o.Id] : new List<EffortOfAMemberInARequestResponse>()
                }).ToListAsync(cancellationToken);
            return new Page<GetOverTimeGroupByUserEffortOfARequestQueryResponse>(result, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
