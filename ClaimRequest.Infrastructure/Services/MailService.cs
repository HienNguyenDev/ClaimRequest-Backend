using System.Net;
using System.Net.Mail;
using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Domain.Common.DTO;
using ClaimRequest.Infrastructure.Shared;
using Microsoft.Extensions.Options;

namespace ClaimRequest.Infrastructure.Services;

public class MailService : IMailService
{
    private readonly MailSettings _mailSettings;
    private readonly ClientSettings _clientSettings;

    public MailService(
        IOptions<MailSettings> mailSettingsOptions, 
        IOptions<ClientSettings> clientSettingsOptions)
    {
        _clientSettings = clientSettingsOptions.Value;
        _mailSettings = mailSettingsOptions.Value;
    }

    public bool SendCreateUserEmail(CreateUserEmailBody emailBody, string userEmail)
    {
        try
        {   
  
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "VERIFY ACCOUNT")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{button_link}}", clientUrl + emailBody.ChangePasswordEndpoint)
                .Replace("{{button_text}}", emailBody.ButtonName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendClaimStatusEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "CLAIM NOTIFICATION")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendClaimCreatedEmail(EmailBody emailBody, string userEmail, List<string> ccEmails)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "NEW CLAIM NOTIFICATION")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO CLAIM")
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            foreach (var ccEmail in ccEmails)
            {
                mail.CC.Add(ccEmail);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };
            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendCreatedRequestOTEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "NEW OVERTIME REQUEST")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO CLAIM")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendRequestOTApprovedEmail(EmailBody emailBody, string userEmail, List<string> ccEmails)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "OT REQUEST NOTIFICATION")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO CLAIM")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            foreach (var ccEmail in ccEmails)
            {
                mail.CC.Add(ccEmail);
            }
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendRequestOTRejectedEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "OT REQUEST NOTIFICATION")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO CLAIM")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendOTEffortStatusChangedEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "OT EFFORT NOTIFICATION")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO EFFORT")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendClaimToApproveEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "NEW CLAIM TO APPROVE")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO CLAIM")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; 

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendClaimToPaidEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "NEW CLAIM TO PAID")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendCreatedEffortEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "NEW OVERTIME EFFORT TO CONFIRM")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO EFFORT")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }

    public bool SendEffortToPaidEmail(EmailBody emailBody, string userEmail)
    {
        try
        {
            var currentYear = DateTime.Now.Year;
            var clientUrl = _clientSettings.ClientUrl;
            Console.WriteLine(emailBody);
            Console.WriteLine(clientUrl);
            string fromEmail = _mailSettings.SmtpUsername;
            string toEmail = userEmail;

            string subject = emailBody.Header;
            string htmlBody = emailBody.Content;
            string body = htmlBody
                .Replace("{{content}}", emailBody.MainContent)
                .Replace("{{header}}", "NEW OVERTIME EFFORT TO PAID")
                .Replace("{{button_link}}", clientUrl)
                .Replace("{{button_text}}", "GO TO EFFORT")
                .Replace("{{username}}", emailBody.User.FullName)
                .Replace("{{sender_title}}", "Admin")
                .Replace("{{sender_name}}", "System")
                .Replace("{{year}}", currentYear.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            string smtpServer = _mailSettings.SmtpServer;
            int smtpPort = _mailSettings.SmtpPort;

            // Configure SMTP Client
            SmtpClient smtp = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, _mailSettings.SmtpPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            // Send the email
            smtp.Send(mail);
            Console.WriteLine("Email Sent Successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error sending email: " + ex.Message);
            return false;
        }
    }
}