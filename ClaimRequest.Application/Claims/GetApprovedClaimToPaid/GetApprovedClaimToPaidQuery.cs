using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Claims.GetApprovedClaimToPaid
{
    public class GetApprovedClaimToPaidQuery : IPageableQuery, IQuery<Page<GetApprovedClaimToPaidResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
