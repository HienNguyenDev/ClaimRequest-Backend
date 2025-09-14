using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeEffort
{
    public sealed class ApproveOverTimeEffortCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
