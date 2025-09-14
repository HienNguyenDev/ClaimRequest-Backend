using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeRequest
{
    public sealed class ApproveOverTimeRequestCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
