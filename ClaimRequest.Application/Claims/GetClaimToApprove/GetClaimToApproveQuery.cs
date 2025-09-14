using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Claims.GetClaimToApprove
{
    public class GetClaimToApproveQuery : IPageableQuery, IQuery<Page<GetCLaimToApproveResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
