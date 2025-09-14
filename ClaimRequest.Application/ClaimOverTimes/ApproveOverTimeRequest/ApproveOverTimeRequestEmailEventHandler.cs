using ClaimRequest.Domain.ClaimOverTime.Events;
using MediatR;
using ClaimRequest.Application.ClaimOverTimes.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeRequest
{
    public class ApproveOverTimeRequestEmailEventHander(OvertimeEventPublisher publisher) 
        : INotificationHandler<OverTimeRequestApprovedEvent>
    {
        public async Task Handle(OverTimeRequestApprovedEvent notification, CancellationToken cancellationToken)
        {
            await publisher.PublishOvertimeRequestApprovedEventAsync(notification);
        }
    }
}
