using ClaimRequest.Domain.ClaimOverTime.Events;
using MediatR;
using ClaimRequest.Application.ClaimOverTimes.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.CreateOverTimeRequest
{
    public class CreateOverTimeEventHandler(
        OvertimeEventPublisher publisher) : INotificationHandler<OverTimeRequestCreatedEvent>
    {
        public async Task Handle(OverTimeRequestCreatedEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishOvertimeRequestCreatedEventAsync(notification);
        }
    }
}
