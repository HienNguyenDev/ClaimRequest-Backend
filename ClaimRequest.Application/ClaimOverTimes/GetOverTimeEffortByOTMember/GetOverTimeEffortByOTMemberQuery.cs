using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortByOTMember
{
    public class GetOverTimeEffortByOTMemberQuery : IPageableQuery, IQuery<Page<GetOverTimeEffortByOTMemberResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        
        public OverTimeEffortStatus Status { get; init; }
    }
}
