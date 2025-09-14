namespace ClaimRequest.Application.Users.Login;

public sealed record TokenResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}