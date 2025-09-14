using ClaimRequest.Domain.Common;
using MediatR;

namespace ClaimRequest.Domain.ClaimOverTime.Events;

public class OverTimeRequestCreatedEvent : IDomainEvent 
{
    public Guid BuleadId { get; set; }
    public string UserName { get; set; }
}