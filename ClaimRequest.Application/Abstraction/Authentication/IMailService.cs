using ClaimRequest.Domain.Common.DTO;

namespace ClaimRequest.Application.Abstraction.Authentication;

public interface IMailService
{
    bool SendCreateUserEmail(CreateUserEmailBody emailBody, string userEmail);
    bool SendClaimStatusEmail(EmailBody emailBody, string userEmail);
    bool SendClaimCreatedEmail(EmailBody emailBody, string userEmail, List<string> ccEmails);
    bool SendCreatedRequestOTEmail(EmailBody emailBody, string userEmail);
    bool SendRequestOTApprovedEmail(EmailBody emailBody, string userEmail, List<string> ccEmails);
    bool SendRequestOTRejectedEmail(EmailBody emailBody, string userEmail);
    bool SendOTEffortStatusChangedEmail(EmailBody emailBody, string userEmail);
    bool SendClaimToApproveEmail(EmailBody emailBody, string userEmail);
    bool SendClaimToPaidEmail(EmailBody emailBody, string userEmail);
    bool SendCreatedEffortEmail(EmailBody emailBody, string userEmail);
    bool SendEffortToPaidEmail(EmailBody emailBody, string userEmail);
}