using ClaimRequest.Domain.Users;

namespace ClaimRequest.Application.Abstraction.Authentication;

public interface ITokenProvider
{
    string Create(User user);
    string GenerateRefreshToken();
}
