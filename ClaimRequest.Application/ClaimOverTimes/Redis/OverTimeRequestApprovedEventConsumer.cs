using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.ClaimOverTimes.Redis;

public class OverTimeRequestApprovedEventConsumer(
        IDatabase redisDb,
        IMailService mailService, 
        IServiceProvider serviceProvider)
{

    public async Task ConsumeEventAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
            
            var entries = await redisDb.StreamReadAsync("overtime_request_approved_events", "0-0", count: 10);
            foreach (var entry in entries)
            {
                string jsonData = entry["event"];
                OverTimeRequestApprovedEvent eventData =
                    JsonConvert.DeserializeObject<OverTimeRequestApprovedEvent>(jsonData);
                await SendOvertimeRequestApprovedEmail(eventData, context, cancellationToken);
                await redisDb.StreamDeleteAsync("overtime_request_approved_events", [entry.Id]);

            }
            await Task.Delay(1000, cancellationToken);
            
        }
    }


    private async Task SendOvertimeRequestApprovedEmail(OverTimeRequestApprovedEvent notification, IDbContext context,  CancellationToken cancellationToken)
    {
        var request = await context.OverTimeRequests
            .Include(r => r.CreatedByUser)
            .Include(r => r.Approver)
            .Include(c => c.OverTimeMembers)
            .ThenInclude(otm => otm.User)
            .FirstOrDefaultAsync(r => r.Id == notification.RequestId);
        if (request == null)
        {
            return;
        }

        var members = request.OverTimeMembers.ToList();

        //var user = await context.Users.Select(u => u.Id).ToListAsync();
        //if (user == null)
        //{
        //    return;
        //}
        var UserCreatedId = request.CreatedByUser;
        var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 5, cancellationToken);
        if (emailTemplate == null)
        {
            return;
        }

        string ContentBody = emailTemplate.MainContent
            .Replace("{{user_created}}", request.CreatedByUser.FullName)
            .Replace("{{approver}}", request.Approver.FullName);

        var emailBody = new EmailBody
        {
            Content = emailTemplate.Content,
            Header = emailTemplate.Header,
            MainContent = ContentBody,
            User = UserCreatedId
        };
        var ccEmails = request.OverTimeMembers
            .Select(otm => otm.User.Email)
            .ToList();
        mailService.SendRequestOTApprovedEmail(emailBody, UserCreatedId.Email, ccEmails);
    }
}