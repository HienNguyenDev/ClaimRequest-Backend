using ClaimRequest.Domain.Users;

namespace ClaimRequest.Application.Users.CreateUser;

public sealed record CreateUserCommandResponse
{
    public Guid Id { get; init; }
    public string FullName { get; init; }
    public string DepartmentName { get; init; }
    public string Email { get; init; }
    public UserRole Role { get; init; }
    public UserRank Rank { get; init; }
    public decimal BaseSalary { get; init; }
    public Guid DepartmentId { get; init; }
}