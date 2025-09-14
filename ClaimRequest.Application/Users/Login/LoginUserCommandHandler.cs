using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.Login;

public sealed class LoginUserCommandHandler
        (IDbContext context, 
        IPasswordHasher passwordHasher, 
        ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users.Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.Email.Trim().ToLower() == command.Email.Trim().ToLower(), cancellationToken);
        if (user is null)
        {
            return Result.Failure<TokenResponse>(UserErrors.NotFoundByEmail);
        }
        
        bool verified = passwordHasher.Verify(command.Password, user.Password);

        if (!verified)
        {
            return Result.Failure<TokenResponse>(UserErrors.WrongPassword);
        }
        
        if (user.Status == UserStatus.InActive)
        {
            return Result.Failure<TokenResponse>(UserErrors.InActive);
        }

        if(user.IsVerified == false)
        {
            return Result.Failure<TokenResponse>(UserErrors.IsNotVerified);
        }

        string accessToken = tokenProvider.Create(user);
        
        
        RefreshToken refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = tokenProvider.GenerateRefreshToken(),
            Expires = DateTime.UtcNow.AddDays(1),
            
        };
        context.RefreshTokens.RemoveRange(user.RefreshTokens);
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync(cancellationToken);
        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
        };
    }
}