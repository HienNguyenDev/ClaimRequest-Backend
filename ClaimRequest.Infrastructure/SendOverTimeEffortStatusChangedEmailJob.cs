using ClaimRequest.Application.ClaimOverTimes.Redis;
using Quartz;

namespace ClaimRequest.Infrastructure;

[DisallowConcurrentExecution]
public class SendOverTimeEffortStatusChangedEmailJob(OverTimeEffortStatusChangedEventConsumer consumer) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await consumer.ConsumeEventAsync(context.CancellationToken);
    }
}