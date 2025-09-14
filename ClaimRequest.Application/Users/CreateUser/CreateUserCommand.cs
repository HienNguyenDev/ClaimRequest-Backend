using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Application.Users.CreateUser;


public sealed class CreateUserCommand : ICommand<CreateUserCommandResponse>
{
    public string Email { get; init; } 
    public string FullName { get; init; }
    public Guid DepartmentId { get; init; }

    public UserRole Role { get; init; }
    public UserRank Rank { get; init; }
    public decimal BaseSalary { get; init; }
}