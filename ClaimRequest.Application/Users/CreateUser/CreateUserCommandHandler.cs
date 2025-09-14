using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using ClaimRequest.Domain.Users.Events;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.CreateUser;

public class CreateUserCommandHandler
        (IDbContext context, 
         IPasswordHasher passwordHasher
         ): ICommandHandler<CreateUserCommand, CreateUserCommandResponse>
{
    public async Task<Result<CreateUserCommandResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email.Trim().ToLower() == command.Email.Trim().ToLower(), cancellationToken))
        {
            return Result.Failure<CreateUserCommandResponse>(UserErrors.EmailNotUnique);
        }

        var department = await context.Departments.FirstOrDefaultAsync(d => d.Id == command.DepartmentId, cancellationToken);
        /*if (await context.Departments.AnyAsync(d => d.Id == command.DepartmentId, cancellationToken) is false)
        {
            return Result.Failure<CreateUserCommandResponse>(DepartmentErrors.NotFound(command.DepartmentId));
        }*/
        
        if (department == null)
        {
            return Result.Failure<CreateUserCommandResponse>(DepartmentErrors.NotFound(command.DepartmentId));
        }
        
        if (command.Role == UserRole.BULeader)
        {
            bool hasBuleader = await context.Users.AnyAsync(u => u.DepartmentId == command.DepartmentId && u.Role == UserRole.BULeader, cancellationToken);
            if (hasBuleader)
            {
                return Result.Failure<CreateUserCommandResponse>(UserErrors.BuLeaderAlreadyExist);
            }
        }
        
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = command.Email.Trim().ToLower(),
            FullName = command.FullName,
            Password = passwordHasher.Hash("123456789"),
            BaseSalary = command.BaseSalary,
            DepartmentId = command.DepartmentId,
            Rank = command.Rank,
            Role = command.Role,
        };
        
        user.Raise(new UserCreatedDomainEvent(user.Id));

        context.Users.Add(user);

        await context.SaveChangesAsync(cancellationToken);

        return new CreateUserCommandResponse
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            BaseSalary = user.BaseSalary,
            Rank = user.Rank,
            Role = user.Role,
            DepartmentId = user.DepartmentId,
            DepartmentName = department.Name,
        };
    }
}