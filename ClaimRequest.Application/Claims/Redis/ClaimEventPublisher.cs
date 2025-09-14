using System.Text;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.Claims.Redis;

public class ClaimEventPublisher(IDatabase redisDb)
{
    
    public async Task PublishClaimStatusChangedEventAsync(ClaimStatusChangedDomainEvent claimEvent) {
        await redisDb.StreamAddAsync("claim_status_changed_events", new NameValueEntry[]
        {
            new NameValueEntry("event", JsonConvert.SerializeObject(claimEvent)),  
        });
        
    }
    
    public async Task PublishClaimCreatedEventAsync(ClaimCreatedDomainEvent claimEvent) {
        await redisDb.StreamAddAsync("claim_created_events", new NameValueEntry[]
        {
            new NameValueEntry("event", JsonConvert.SerializeObject(claimEvent)),  
        });
        
    }
    
    
}