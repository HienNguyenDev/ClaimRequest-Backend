using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Users.CreateDepartment;

public sealed class CreateDepartmentCommand : ICommand<CreateDepartmentCommandResponse>
{
    public string Name { get; init; } 
    public string Code { get; init; } 
    public string Description { get; init; } 
}