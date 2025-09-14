using System.Text.Json.Serialization;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Application.Users.UpdateUsers;

public sealed class UpdateUserCommand : ICommand<UpdateUserResponse>
{
    
    public Guid UserId { get; set; } 
    public string? FullName { get; set; } 
    public string? Email { get; set; } 
    public UserRole? Role { get; set; }
    public UserRank? Rank { get; set; }
    public decimal? BaseSalary { get; set; }
    public UserStatus? Status { get; set; }
    public Guid? DepartmentId { get; set; }
}