using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.Login;

public class LoginUserWithRefreshToken
        (IDbContext context, 
        ITokenProvider tokenProvider) 
        : ICommandHandler<LoginUserWithRefreshToken.LoginByRefreshTokenCommand , TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginByRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken, cancellationToken);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            return Result.Failure<TokenResponse>(UserErrors.InvalidRefreshToken);

        }

        string accessToken = tokenProvider.Create(refreshToken.User);
        refreshToken.Token = tokenProvider.GenerateRefreshToken();
        refreshToken.Expires = DateTime.UtcNow.AddDays(1);
        
        await context.SaveChangesAsync(cancellationToken);
        return new TokenResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }
        
        
    public sealed class LoginByRefreshTokenCommand : ICommand<TokenResponse>
    {
        public string RefreshToken { get; set; }
    }
}

    

