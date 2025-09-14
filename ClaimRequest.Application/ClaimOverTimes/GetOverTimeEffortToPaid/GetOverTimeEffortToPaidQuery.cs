using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;

using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToPaid;

public class GetOverTimeEffortToPaidQuery : IPageableQuery, IQuery<Page<GetOverTimeEffortToPaidResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}