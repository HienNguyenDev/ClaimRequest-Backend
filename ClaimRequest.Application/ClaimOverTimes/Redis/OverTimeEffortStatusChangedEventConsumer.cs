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

public class OverTimeEffortStatusChangedEventConsumer(
    IDatabase redisDb,
    IMailService mailService, 
    IServiceProvider serviceProvider)
{



    public async Task ConsumeEventAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();
            var entries = await redisDb.StreamReadAsync("overtime_effort_status_changed_events", "0-0", count: 10);
            foreach (var entry in entries)
            {
                string jsonData = entry["event"];
                OverTimeEffortStatusChangedEvent eventData =
                    JsonConvert.DeserializeObject<OverTimeEffortStatusChangedEvent>(jsonData);
                await SendOverTimeEffortStatusChangeEmail(eventData, context, cancellationToken);
                await redisDb.StreamDeleteAsync("overtime_effort_status_changed_events", [entry.Id]);

            }
            await Task.Delay(1000, cancellationToken);
        }
    }


    private async Task SendOverTimeEffortStatusChangeEmail(OverTimeEffortStatusChangedEvent notification,
        IDbContext context, CancellationToken cancellationToken)
    {
        var effort = await context.OverTimeEffort
            .Include(t => t.OverTimeDate)
            .Include(m => m.OverTimeMember)
            .ThenInclude(c => c.Request)
            .FirstOrDefaultAsync(u => u.Id == notification.EffortId, cancellationToken);
        if (effort == null)
        {
            return;
        }

        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == effort.OverTimeMember.UserId,
            cancellationToken);
        if (user == null)
        {
            return;
        }

        var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 7, cancellationToken);
        if (emailTemplate == null)
        {
            return;
        }

        var userCreateEffortId = effort.OverTimeMember.UserId;
        var userCreateEffortName = await context.Users
            .Where(u => u.Id == userCreateEffortId)
            .Select(n => n.FullName)
            .FirstOrDefaultAsync();

        var userActionId = notification.UserId;

        var userActionName = context.Users
            .FirstOrDefault(u => u.Id == userActionId)?
            .FullName;

        string ContentBody = emailTemplate.MainContent
            .Replace("{{status}}", notification.Action)
            .Replace("{{user_action}}", userActionName)
            .Replace("{{user_name}}", userCreateEffortName)
            .Replace("{{task_description}}", effort.TaskDescription)
            .Replace("{{date}}", effort.OverTimeDate.Date.ToString("dd-MM-yyyy"))
            .Replace("{{hours}}", (effort.DayHours + effort.NightHours).ToString());

        var emailBody = new EmailBody()
        {
            Content = emailTemplate.Content,
            Header = emailTemplate.Header,
            MainContent = ContentBody,
            User = user
        };
        mailService.SendOTEffortStatusChangedEmail(emailBody, user.Email);

        if (notification.Action.Equals("Confirmed"))
        {
            var emailBULTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 8, cancellationToken);
            if (emailBULTemplate == null)
            {
                return;
            }

            var approver =
                await context.Users.FirstOrDefaultAsync(c => c.Id == effort.OverTimeMember.Request.ApproverId,
                    cancellationToken);
            if (approver == null)
            {
                return;
            }
            string ContentBULBody = emailBULTemplate.MainContent
                .Replace("{{status}}", notification.Action)
                .Replace("{{user_action}}", userActionName)
                .Replace("{{user_name}}", userCreateEffortName)
                .Replace("{{task_description}}", effort.TaskDescription)
                .Replace("{{date}}", effort.OverTimeDate.Date.ToString("dd-MM-yyyy"))
                .Replace("{{hours}}", (effort.DayHours + effort.NightHours).ToString());
            var emailBodyBUL = new EmailBody
            {
                Content = emailBULTemplate.Content,
                Header = emailBULTemplate.Header,
                MainContent = ContentBULBody,
                User = approver
            };

            mailService.SendOTEffortStatusChangedEmail(emailBodyBUL, approver.Email);
        }

        if (notification.Action.Equals("Approved"))
        {
            var emailFinanceTemplate =
                await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 13, cancellationToken);
            if (emailFinanceTemplate == null)
            {
                return;
            }

            var finance = await context.Users.SingleOrDefaultAsync(u => u.Role == UserRole.Finance);
            if (finance == null)
            {
                return;
            }
            string ContentPaidBody = emailFinanceTemplate.MainContent
                .Replace("{{status}}", notification.Action)
                .Replace("{{user_action}}", userActionName)
                .Replace("{{user_name}}", userCreateEffortName)
                .Replace("{{task_description}}", effort.TaskDescription)
                .Replace("{{date}}", effort.OverTimeDate.Date.ToString("dd-MM-yyyy"))
                .Replace("{{hours}}", (effort.DayHours + effort.NightHours).ToString());
            var emailBodyFinance = new EmailBody
            {
                Content = emailFinanceTemplate.Content,
                Header = emailFinanceTemplate.Header,
                MainContent = ContentPaidBody,
                User = finance
            };

            mailService.SendEffortToPaidEmail(emailBodyFinance, finance.Email);
        }
    }
}