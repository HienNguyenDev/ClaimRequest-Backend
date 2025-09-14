using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeGroupByUserEffortOfARequest
{
    public class GetOverTimeGroupByUserEffortOfARequestQuery : IPageableQuery, IQuery<Page<GetOverTimeGroupByUserEffortOfARequestQueryResponse>>
    {
        public Guid Id { get; set; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
