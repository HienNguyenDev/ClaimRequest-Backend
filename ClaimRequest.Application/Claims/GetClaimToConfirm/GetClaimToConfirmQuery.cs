using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Claims.GetClaimToConfirm
{
    public class GetClaimToConfirmQuery : IPageableQuery, IQuery<Page<GetClaimToConfirmQueryResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
