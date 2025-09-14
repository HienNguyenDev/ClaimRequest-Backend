using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.ActivateUser;

public class ActivateUserCommandHandler(
    IDbContext context,
    IPasswordHasher passwordHasher) : ICommandHandler<ActivateUserCommand, ActivateUserCommandResponse>
{
    public async Task<Result<ActivateUserCommandResponse>> Handle(ActivateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);
        if (user == null)
        {
            return Result.Failure<ActivateUserCommandResponse>(UserErrors.NotFoundByEmail);
        }

        if (!user.IsVerified)
        {
            user.IsVerified = true;
            user.Password = passwordHasher.Hash(command.NewPassword);
        }
        
        await context.SaveChangesAsync(cancellationToken);
        return Result.Success(new ActivateUserCommandResponse()
        {
            Email = command.Email
        });
    }
}