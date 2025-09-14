using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.Redis;

public class OverTimeRequestCreatedEventConsumer(
        IDatabase redisDb,
        IMailService mailService, 
        IServiceProvider serviceProvider)
{

    public async Task ConsumeEventAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
            
            var entries = await redisDb.StreamReadAsync("overtime_request_created_events", "0-0", count: 10);
            foreach (var entry in entries)
            {
                string jsonData = entry["event"];
                OverTimeRequestCreatedEvent eventData =
                    JsonConvert.DeserializeObject<OverTimeRequestCreatedEvent>(jsonData);
                await SendOvertimeRequestCreatedEmail(eventData, context, cancellationToken);
                await redisDb.StreamDeleteAsync("overtime_request_created_events", [entry.Id]);

            }
            await Task.Delay(1000, cancellationToken);
        }
    }
    


    private async Task SendOvertimeRequestCreatedEmail(OverTimeRequestCreatedEvent notification, IDbContext context,  CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == notification.BuleadId, cancellationToken);
        if (user == null)
        {
            return;
        }

        var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 4, cancellationToken);
        if (emailTemplate == null)
        {
            return;
        }

        string MainContentBody = emailTemplate.MainContent
            .Replace("{{user_name}}", notification.UserName);
        
        var emailBody = new EmailBody()
        {
            Content = emailTemplate.Content,
            Header = emailTemplate.Header,
            MainContent = MainContentBody,
            User = user
        };

        mailService.SendCreatedRequestOTEmail(emailBody, user.Email);
    }
}