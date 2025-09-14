using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Projects.GetProjectMembers;

public sealed class GetProjectMembersQuery : IQuery<List<GetProjectMembersResponse>>
{
    public Guid ProjectId { get; }

    public GetProjectMembersQuery(Guid projectId)
    {
        ProjectId = projectId;
    }

    
}