using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using ClaimRequest.Domain.Users.Events;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.CreateDepartment;

public class CreateDepartmentCommandHandler(IDbContext context) : ICommandHandler<CreateDepartmentCommand, CreateDepartmentCommandResponse>
{
    public async Task<Result<CreateDepartmentCommandResponse>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        if (await context.Departments.AnyAsync(d => d.Code == command.Code, cancellationToken))
        {
            return Result.Failure<CreateDepartmentCommandResponse>(DepartmentErrors.CodeNotUnique);
        }
        
        var department = new Department()
        {
            Id = Guid.NewGuid(),
            Code = command.Code,
            Name = command.Name,
            Description = command.Description,
        };

        department.Raise(new DeparmentCreatedDomainEvent(department.Id));

        context.Departments.Add(department);

        await context.SaveChangesAsync(cancellationToken);

        return new CreateDepartmentCommandResponse
        {
            Id = department.Id,
            Code = department.Code,
            Name = department.Name,
            Description = department.Description,
        };
    }
}