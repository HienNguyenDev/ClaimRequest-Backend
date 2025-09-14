using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.ClaimOverTimes.ConfirmOverTimeEffort
{
    public sealed class ConfirmOverTimeEffortCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}