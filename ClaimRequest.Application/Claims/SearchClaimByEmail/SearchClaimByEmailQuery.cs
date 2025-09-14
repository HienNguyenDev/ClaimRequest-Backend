using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Claims.SearchClaimByEmail
{
    public class SearchClaimByEmailQuery : IPageableQuery, IQuery<Page<SearchClaimByEmailResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? Email { get; set; }       
    }
}
