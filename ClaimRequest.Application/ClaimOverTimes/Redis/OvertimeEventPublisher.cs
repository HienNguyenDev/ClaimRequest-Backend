using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Events;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.Redis;

public class OvertimeEventPublisher(IDatabase redisDb)
{
    public async Task PublishOvertimeEffortStatusChangedEventAsync(OverTimeEffortStatusChangedEvent overtimeEffotrEvent ) {
        await redisDb.StreamAddAsync("overtime_effort_status_changed_events", new NameValueEntry[]
        {
            new NameValueEntry("event", JsonConvert.SerializeObject(overtimeEffotrEvent)),  
        });
    }
    
    public async Task PublishOvertimeRequestCreatedEventAsync( OverTimeRequestCreatedEvent overtimeEvent) {
        await redisDb.StreamAddAsync("overtime_request_created_events", new NameValueEntry[]
        {
            new NameValueEntry("event", JsonConvert.SerializeObject(overtimeEvent)),  
        });
    }
    
    public async Task PublishOvertimeRequestApprovedEventAsync( OverTimeRequestApprovedEvent overtimeEvent) {
        await redisDb.StreamAddAsync("overtime_request_approved_events", new NameValueEntry[]
        {
            new NameValueEntry("event", JsonConvert.SerializeObject(overtimeEvent)),  
        });
    }
    
    public async Task PublishOvertimeEffortCreatedEventAsync( OverTimeEffortCreatedEvent overtimeEvent) {
        await redisDb.StreamAddAsync("overtime_effort_created_events", new NameValueEntry[]
        {
            new NameValueEntry("event", JsonConvert.SerializeObject(overtimeEvent)),  
        });
    }
    
}