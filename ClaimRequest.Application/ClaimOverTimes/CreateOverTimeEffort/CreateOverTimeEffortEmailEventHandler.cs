using ClaimRequest.Domain.ClaimOverTime.Events;
using MediatR;

using ClaimRequest.Application.ClaimOverTimes.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.CreateOverTimeEffort
{
    public class CreateOverTimeEffortEmailEventHandler(OvertimeEventPublisher publisher) 
        : INotificationHandler<OverTimeEffortCreatedEvent>
    {
        public async Task Handle(OverTimeEffortCreatedEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishOvertimeEffortCreatedEventAsync(notification);
        }    
    }
}
