using ClaimRequest.Application.ClaimOverTimes.Redis;
using ClaimRequest.Domain.ClaimOverTime.Events;
using DocumentFormat.OpenXml.Bibliography;
using Quartz;

namespace ClaimRequest.Infrastructure;

[DisallowConcurrentExecution]
public class SendOverTimeRequestCreatedEmailJob(OverTimeRequestCreatedEventConsumer consumer) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await consumer.ConsumeEventAsync(context.CancellationToken);
    }
}