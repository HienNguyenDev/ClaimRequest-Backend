using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Claims.Redis;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common.DTO;

using MediatR;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.EmailEventHandler.ClaimsStatusChanged
{
    public class ClaimsStatusChangeEventHander
        (ClaimEventPublisher publisher) 
        : INotificationHandler<ClaimStatusChangedDomainEvent>
    {
        
        public async Task Handle(ClaimStatusChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishClaimStatusChangedEventAsync(notification);
        }
    }
}
