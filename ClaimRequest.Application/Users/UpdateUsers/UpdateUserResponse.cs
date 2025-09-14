using ClaimRequest.Domain.Users;

namespace ClaimRequest.Application.Users.UpdateUsers;

public class UpdateUserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public UserRank Rank { get; set; }
    public decimal BaseSalary { get; set; }
    public UserStatus Status { get; set; }
    public Guid DepartmentId { get; set; }
    
    public UpdateUserResponse(User user)
    {
        Id = user.Id;
        FullName = user.FullName;
        Email = user.Email;
        Role = user.Role;
        Rank = user.Rank;
        BaseSalary = user.BaseSalary;
        Status = user.Status;
        DepartmentId = user.DepartmentId;
    }
}