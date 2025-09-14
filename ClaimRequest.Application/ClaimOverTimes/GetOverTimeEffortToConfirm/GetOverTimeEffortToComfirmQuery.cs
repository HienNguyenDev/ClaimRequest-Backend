using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToConfirm;

public class GetOverTimeEffortToComfirmQuery : IPageableQuery, IQuery<Page<GetOverTimeEffortToConfirmResponse>>
{
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}