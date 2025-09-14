using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Claims.ConfirmClaim
{
    public sealed class ConfirmClaimCommand : ICommand<Guid> 
    {
        public Guid Id { get; set; }
    }
}
