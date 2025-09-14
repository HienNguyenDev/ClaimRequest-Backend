using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeDateToDeclareEffort
{
    public class GetOverTimeDateToDeclareEffortQueryHandler(IDbContext context, IUserContext userContext) : IQueryHandler<GetOverTimeDateToDeclareEffortQuery, List<GetOverTimeDateToDeclareEffortQueryResponse>>
    {
        public async Task<Result<List<GetOverTimeDateToDeclareEffortQueryResponse>>> Handle(GetOverTimeDateToDeclareEffortQuery request, CancellationToken cancellationToken)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var dateList = await context.OverTimeDates.Where(d => d.OverTimeRequestId == request.Id && d.Date < today).ToListAsync();

            var userId = userContext.UserId;
            var dateListWithEffort = await context.OverTimeEffort.Where(e => e.OverTimeMember.RequestId == request.Id && e.OverTimeMember.UserId == userId && e.OverTimeDate.OverTimeRequestId == request.Id)
                                                                 .Select(e => e.OverTimeDate.Date)
                                                                 .Distinct()
                                                                 .ToListAsync();
            dateList.RemoveAll(d => dateListWithEffort.Contains(d.Date));

            var result = dateList.Select(d => new GetOverTimeDateToDeclareEffortQueryResponse
            {
                OverTimeDateId = d.Id,
                Date = d.Date
            }).ToList();
            return result;
        }
    }
}
