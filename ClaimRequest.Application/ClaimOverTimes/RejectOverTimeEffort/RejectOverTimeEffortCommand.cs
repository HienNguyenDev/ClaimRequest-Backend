using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.ClaimOverTimes.RejectOverTimeEffort;

public class RejectOverTimeEffortCommand : ICommand<string>
{
    public Guid Id { get; set; }
}