namespace ClaimRequest.Domain.Common.DTO;

public class CreateUserEmailBody : EmailBody
{
    public string ChangePasswordEndpoint { get; set; }
    public string ButtonName { get; set; }
}