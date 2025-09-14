using ClaimRequest.Application.Claims.Redis;
using Quartz;

namespace ClaimRequest.Infrastructure;

[DisallowConcurrentExecution]
public class SendClaimCreateEmailJob(ClaimCreatedEventConsumer consumer) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await consumer.ConsumeEventAsync(context.CancellationToken);
    }
}