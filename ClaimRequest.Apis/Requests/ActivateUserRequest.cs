namespace ClaimRequest.Apis.Requests;

public class ActivateUserRequest
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}