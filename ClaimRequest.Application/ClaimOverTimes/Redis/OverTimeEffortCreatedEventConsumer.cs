using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common.DTO;
using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.Redis;

public class OverTimeEffortCreatedEventConsumer(
        IDatabase redisDb,
        IMailService mailService, 
        IServiceProvider serviceProvider)
{

    public async Task ConsumeEventAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
            
            var entries = await redisDb.StreamReadAsync("overtime_effort_created_events", "0-0", count: 10);
            foreach (var entry in entries)
            {
                string jsonData = entry["event"]; 
                OverTimeEffortCreatedEvent eventData =
                    JsonConvert.DeserializeObject<OverTimeEffortCreatedEvent>(jsonData);
                await SendOvertimeEffortCreatedEmail(eventData, context, cancellationToken);
                await redisDb.StreamDeleteAsync("overtime_effort_created_events", [entry.Id]);
            }
        }
    }


    private async Task SendOvertimeEffortCreatedEmail(OverTimeEffortCreatedEvent notification, IDbContext context,  CancellationToken cancellationToken)
    {
        User? user;
        if (notification.UserId == notification.SupervisorId)
        {
            user = await context.Users.FirstOrDefaultAsync(u => u.Id == notification.ApproverId, cancellationToken);
        }
        else
        {
            user = await context.Users.FirstOrDefaultAsync(u => u.Id == notification.SupervisorId, cancellationToken);
        }
        
        if  (user == null)
        {
            return;
        }
        var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 12, cancellationToken);
        if (emailTemplate == null)
        {
            return;
        }
        
        
        var UserCreateEffortName = context.Users
            .FirstOrDefault(u => u.Id == notification.UserId)?
            .FullName;
        string MainContentBody = emailTemplate.MainContent
            .Replace("{{user_name}}", UserCreateEffortName);

        var emailBody = new EmailBody()
        {
            Content = emailTemplate.Content,
            Header = emailTemplate.Header,
            MainContent = MainContentBody,
            User = user
        };

        mailService.SendCreatedEffortEmail(emailBody, user.Email);
    }
}