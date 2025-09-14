using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Claims.GetPersonalByStatusClaim
{
    public class GetPersonalByStatusClaimQuery : IPageableQuery, IQuery<Page<GetPersonalByStatusClaimQueryResponse>>
    {
        public ClaimStatus Status { get; set; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        public List<InformToResponse>? InformTos { get; set; }
    }
    public sealed record InformToResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
    }




}
