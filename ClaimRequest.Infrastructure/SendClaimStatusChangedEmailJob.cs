using ClaimRequest.Application.Claims.Redis;
using Quartz;

namespace ClaimRequest.Infrastructure;

[DisallowConcurrentExecution]
public class SendClaimStatusChangedEmailJob(ClaimStatusChangeEventConsumer consumer) : IJob
{
    public async Task Execute(IJobExecutionContext context )
    {
        await consumer.ConsumeEventAsync(cancellationToken : context.CancellationToken);
    }
}