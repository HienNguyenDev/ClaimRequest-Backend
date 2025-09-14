using ClaimRequest.Domain.Common;
using MediatR;

namespace ClaimRequest.Domain.Claims.Events;

public sealed record ClaimHasFeePaidEvent : IDomainEvent
{
    public Guid ClaimId { get; set; }
    
}