using System.Text.Json.Serialization;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Apis.Requests;

public class UpdateUserRequest
{
    public Guid UserId { get; set; } 
    public string? FullName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public UserRole? Role { get; set; }
    public UserRank? Rank { get; set; }
    public decimal? BaseSalary { get; set; }
    public UserStatus? Status { get; set; }
    public Guid? DepartmentId { get; set; }
}