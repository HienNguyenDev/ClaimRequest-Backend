using ClaimRequest.Domain.Users;

namespace ClaimRequest.Apis.Requests;

public sealed class CreateUserRequest
{
    public string Email { get; set; } 
    public string FullName { get; set; }
    public Guid Department { get; set; }
    /*
    public string Password { get; set; }
    */
    public UserRole Role { get; set; }
    public UserRank Rank { get; set; }
    public decimal BaseSalary { get; set; }
}