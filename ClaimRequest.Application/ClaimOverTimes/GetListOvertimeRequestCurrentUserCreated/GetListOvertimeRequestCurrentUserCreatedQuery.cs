using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetListOvertimeRequestCurrentUserCreated;

public class GetListOvertimeRequestCurrentUserCreatedQuery : IPageableQuery, IQuery<Page<GetListOvertimeRequestCurrentUserCreatedQueryResponse>>
{
    public Guid Id { get; set; }
    public OverTimeRequestStatus Status { get; set; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
}