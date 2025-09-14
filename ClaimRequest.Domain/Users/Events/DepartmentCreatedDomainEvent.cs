using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Users.Events;

public sealed record DeparmentCreatedDomainEvent(Guid UserId) : IDomainEvent;