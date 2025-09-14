using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.ClaimOverTimes.RejectOverTimeRequest
{
    public class RejectOverTimeRequestEmailEventHander(
        IMailService mailService,
        IDbContext context) : INotificationHandler<OverTimeRequestRejectedEvent>
    {
        public async Task Handle(OverTimeRequestRejectedEvent notification, CancellationToken cancellationToken)
        {
            var request = await context.OverTimeRequests
                .Include(c => c.CreatedByUser)
                .FirstOrDefaultAsync(r => r.Id == notification.RequestId);
            if (request == null)
            {
                return;
            }   

            //var user = await context.Users.Select(u => u.Id).ToListAsync();
            //if (user == null)
            //{
            //    return;
            //}
            var UserCreatedId = request.CreatedByUser;
            var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 6, cancellationToken);
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

            mailService.SendRequestOTRejectedEmail(emailBody, UserCreatedId.Email);

        }
    }
}
