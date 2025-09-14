using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;

public class GetOverTimeEffortToApproveQuery : IPageableQuery, IQuery<Page<GetOverTimeEffortToApproveResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}