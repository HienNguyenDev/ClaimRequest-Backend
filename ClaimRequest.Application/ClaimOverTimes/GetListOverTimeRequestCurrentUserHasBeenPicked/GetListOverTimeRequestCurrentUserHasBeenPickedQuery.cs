using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetListOverTimeRequestCurrentUserHasBeenPicked
{
    public class GetListOverTimeRequestCurrentUserHasBeenPickedQuery : IPageableQuery  , IQuery<Page<GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }

}
