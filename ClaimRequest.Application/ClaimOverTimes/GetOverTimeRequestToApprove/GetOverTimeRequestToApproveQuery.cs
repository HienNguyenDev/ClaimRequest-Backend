using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeRequestToApprove;

public class GetOverTimeRequestToApproveQuery : IPageableQuery, IQuery<Page<GetOverTimeRequestToApproveResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}