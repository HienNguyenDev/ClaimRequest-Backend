using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.Common.DTO;
using ClaimRequest.Domain.Users.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.CreateUser
{
    public class CreateUserCommandEventHandler(
        IMailService mailService, 
        IDbContext context, 
        IUserContext userContext) : INotificationHandler<UserCreatedDomainEvent>
    {
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == notification.UserId, cancellationToken);
            if (user == null)
            {
                return;
            }

            var emailTemplate = await context.EmailTemplates.FirstOrDefaultAsync(e => e.Id == 1, cancellationToken);
            if (emailTemplate == null)
            {
                return;
            }
            var senderName = await context.Users
                .Where(u => u.Id == userContext.UserId)
                .Select(u => u.FullName) 
                .FirstOrDefaultAsync();
            // Thay thế nội dung trong template với dữ liệu thực tế
            string ContentBody = emailTemplate.Content;
                // .Replace("{{header}}", "VERIFY ACCOUNT")
                // .Replace("{{username}}", user.FullName)
                // .Replace("{{button_link}}", $"https://example.com/verify")
                // .Replace("{{button_text}}", "Verify Now")
                // .Replace("{{sender_name}}", senderName)
                // .Replace("{{sender_title}}", "FSA Admin")
                // .Replace("{{year}}", currentYear.ToString());

            var emailBody = new CreateUserEmailBody
            {
                Content = ContentBody, // Nội dung ngắn gọn của email
                Header = emailTemplate.Header,   // Tiêu đề email
                ChangePasswordEndpoint = $"/change-password?email={user.Email}", // Đường link xác thực tài khoản
                ButtonName = "Verify Now",
                MainContent = emailTemplate.MainContent,
                User = user
            };

            mailService.SendCreateUserEmail(emailBody, user.Email);
        }
    }
}
