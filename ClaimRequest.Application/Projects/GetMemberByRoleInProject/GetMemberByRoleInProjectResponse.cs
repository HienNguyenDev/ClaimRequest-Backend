namespace ClaimRequest.Application.Projects.GetMemberByRoleInProject;

public class GetMemberByRoleInProjectResponse
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}