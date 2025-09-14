using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonApplication.GetReasons;

namespace ClaimRequest.Application.ReasonApplication.CreateReason
{
    public sealed class CreateReasonCommand : ICommand<ReasonsResponse>
    {
        public Guid RequestTypeId { get; set; }
        public string Name { get; set; } = null!;
    }
}
