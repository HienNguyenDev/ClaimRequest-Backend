using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Users.Login;

public sealed class LoginUserCommand() : ICommand<TokenResponse>
{
    public string Email { get; init; }
    public string Password { get; init; }
}
