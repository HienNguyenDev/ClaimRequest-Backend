using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.Claims.Redis;

public class ClaimCreatedEventConsumer(IDatabase redisDb, IMailService mailService, IServiceProvider serviceProvider)
{
 
    public async Task ConsumeEventAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();

            var entries = await redisDb.StreamReadAsync("claim_created_events", "0-0", count: 10);
            foreach (var entry in entries)
            {
                string jsonData = entry["event"];
                ClaimCreatedDomainEvent claimEvent =
                    JsonConvert.DeserializeObject<ClaimCreatedDomainEvent>(jsonData);
                await SendEmailClaimCreated(claimEvent, cancellationToken, context);
                await redisDb.StreamDeleteAsync("claim_created_events", [entry.Id]);
            }
            await Task.Delay(1000, cancellationToken);
            
        }
    }
    
    
     private async Task SendEmailClaimCreated(ClaimCreatedDomainEvent notification, CancellationToken cancellationToken, IDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == notification.SupervisorID, cancellationToken);
            if (user == null)
            {
                return;
            }

            var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 3, cancellationToken);
            if (emailTemplate == null)
            {
                return;
            }

            var claim = await context.Claims.FirstOrDefaultAsync(c => c.Id == notification.ClaimId, cancellationToken);
            var reasonName = context.Claims
                    .Where(c => c.Id == claim.Id)
                    .Include(c => c.Reason)
                    .Select(c => c.Reason != null ? c.Reason.Name : "Unknown")
                    .FirstOrDefault();
            var supervisor = await context.Users
                .Where(u => u.Id == claim.SupervisorId)
                .Select(u => u.FullName)
            .FirstOrDefaultAsync();

            var approver = await context.Users
                .Where(u => u.Id == claim.ApproverId)
                .Select(u => u.FullName)
                .FirstOrDefaultAsync();

            var informToUsers = await context.Users
                .Where(u => notification.InformTo.Contains(u.Id))
                .ToListAsync(cancellationToken);

            string MainContentBody = emailTemplate.MainContent
                .Replace("{{user_name}}", notification.UserName)
                .Replace("{{reason_name}}", reasonName)
                .Replace("{{supervisor_name}}", supervisor)
                .Replace("{{approver_name}}", approver)
                .Replace("{{start_date}}", claim.StartDate.ToString("dd-MM-yyyy"))
                .Replace("{{end_date}}", claim.EndDate.ToString("dd-MM-yyyy"))
                .Replace("{{claim_fee}}", claim.ClaimFee.ToString());
            string ContentBody = emailTemplate.Content
                .Replace("{{username}}", supervisor);
            var emailBody = new EmailBody
            {
                Content = ContentBody,
                Header = emailTemplate.Header,
                MainContent = MainContentBody,
                User = user
            };

            var ccEmails = informToUsers.Select(u => u.Email).ToList();
            mailService.SendClaimCreatedEmail(emailBody, user.Email, ccEmails);
        }
}