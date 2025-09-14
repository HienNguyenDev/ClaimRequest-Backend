using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Users.ActivateUser;

public class ActivateUserCommand : ICommand<ActivateUserCommandResponse>
{
    public string Email { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}