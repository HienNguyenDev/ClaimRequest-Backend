using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common.DTO;
using ClaimRequest.Domain.Users;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace ClaimRequest.Application.Claims.Redis;

public class ClaimStatusChangeEventConsumer(IDatabase redisDb, IMailService mailService, IServiceProvider serviceProvider)
{
   
    public async Task ConsumeEventAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {   
            var context = scope.ServiceProvider.GetRequiredService<IDbContext>();

            var entries = await redisDb.StreamReadAsync("claim_status_changed_events", "0-0", count: 10);
            foreach (var entry in entries)
            {
                string jsonData = entry["event"];
                ClaimStatusChangedDomainEvent claimEvent =
                    JsonConvert.DeserializeObject<ClaimStatusChangedDomainEvent>(jsonData);
                await SendEmailClaimStatusChanged(claimEvent, cancellationToken, context);
                await redisDb.StreamDeleteAsync("claim_status_changed_events", [entry.Id]);

            }

            await Task.Delay(1000, cancellationToken);
            
        }
    }
    
    
    public async Task SendEmailClaimStatusChanged(ClaimStatusChangedDomainEvent notification, CancellationToken cancellationToken, IDbContext context)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == notification.UserId, cancellationToken);
            if (user == null)
            {
                return;
            }   

            var claim = await context.Claims.Include(c => c.Approver).Include(c => c.Supervisor).FirstOrDefaultAsync(u => u.Id == notification.ClaimId, cancellationToken);

            var reasonName = context.Claims
                    .Where(c => c.Id == claim.Id)
                    .Include(c => c.Reason)
                    .Select(c => c.Reason != null ? c.Reason.Name : "Unknown")
                    .FirstOrDefault();

            var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 2, cancellationToken);
            if (emailTemplate == null)
            {
                return;
            }

            string ContentBody = emailTemplate.MainContent
                .Replace("{{status}}", notification.Action)
                .Replace("{{user_action}}", notification.UserActionName)
                .Replace("{{reason_name}}", reasonName)
                .Replace("{{supervisor_name}}", claim.Supervisor.FullName)
                .Replace("{{approver_name}}", claim.Approver.FullName)
                .Replace("{{start_date}}", claim.StartDate.ToString("dd-MM-yyyy"))
                .Replace("{{end_date}}", claim.EndDate.ToString("dd-MM-yyyy"))
                .Replace("{{claim_fee}}", claim.ClaimFee.ToString() ?? default);

            var emailBody = new EmailBody()
            {
                Content = emailTemplate.Content, 
                Header = emailTemplate.Header,  
                MainContent = ContentBody,
                User = user
            };
            mailService.SendClaimStatusEmail(emailBody, user.Email);

            if (notification.Action.Equals("Confirmed"))
            {
                var emailApproveTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 10, cancellationToken);
                if (emailApproveTemplate == null)
                {
                    return;
                }
                var approver = await context.Users.FirstOrDefaultAsync(c => c.Id == claim.ApproverId);
                if (approver == null)
                {
                    return;
                }

                string ContentApproveBody = emailApproveTemplate.MainContent
                    .Replace("{{status}}", notification.Action)
                    .Replace("{{user_action}}", notification.UserActionName)
                    .Replace("{{user_name}}", user.FullName)
                    .Replace("{{reason_name}}", reasonName)
                    .Replace("{{supervisor_name}}", claim.Supervisor.FullName)
                    .Replace("{{approver_name}}", claim.Approver.FullName)
                    .Replace("{{start_date}}", claim.StartDate.ToString("dd-MM-yyyy"))
                    .Replace("{{end_date}}", claim.EndDate.ToString("dd-MM-yyyy"))
                    .Replace("{{claim_fee}}", claim.ClaimFee.ToString() ?? default);
                var emailBodyBUL = new EmailBody
                    {
                        Content = emailApproveTemplate.Content,
                        Header = emailApproveTemplate.Header,
                        MainContent = ContentApproveBody,
                        User = approver
                    };

                mailService.SendClaimToApproveEmail(emailBodyBUL, approver.Email);
            }

            if (notification.Action.Equals("Approved") && claim.ClaimFee != 0)
            {
                var emailPaidTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 11, cancellationToken);
                if (emailPaidTemplate == null)
                {
                    return;
                }

                var finance = await context.Users.SingleOrDefaultAsync(u => u.Role == UserRole.Finance);
                if (finance == null)
                {
                    return;
                }
                string ContentPaidBody = emailPaidTemplate.MainContent
                    .Replace("{{status}}", notification.Action)
                    .Replace("{{user_action}}", notification.UserActionName)
                    .Replace("{{reason_name}}", reasonName)
                    .Replace("{{supervisor_name}}", claim.Supervisor.FullName)
                    .Replace("{{approver_name}}", claim.Approver.FullName)
                    .Replace("{{start_date}}", claim.StartDate.ToString("dd-MM-yyyy"))
                    .Replace("{{end_date}}", claim.EndDate.ToString("dd-MM-yyyy"))
                    .Replace("{{claim_fee}}", claim.ClaimFee.ToString() ?? default);
                var emailBodyFinance = new EmailBody
                {
                    Content = emailPaidTemplate.Content,
                    Header = emailPaidTemplate.Header,
                    MainContent = ContentPaidBody,
                    User = finance
                };
                mailService.SendClaimToPaidEmail(emailBodyFinance, finance.Email);
            }
    }
}