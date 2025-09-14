using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Projects.GetMemberByRoleInProject;

public class GetMemberByRoleInProjectQuery : IQuery<List<GetMemberByRoleInProjectResponse>>
{
    public string Role { get; set; }
    public Guid Id { get; set; }
}