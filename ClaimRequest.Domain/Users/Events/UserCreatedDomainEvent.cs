using ClaimRequest.Domain.Common;
using MediatR;

namespace ClaimRequest.Domain.Users.Events;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;





