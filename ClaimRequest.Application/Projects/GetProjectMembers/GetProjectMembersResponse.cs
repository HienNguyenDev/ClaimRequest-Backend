namespace ClaimRequest.Application.Projects.GetProjectMembers;

public class GetProjectMembersResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    
    public int NumberOfOvertimeHours { get; set; }
}