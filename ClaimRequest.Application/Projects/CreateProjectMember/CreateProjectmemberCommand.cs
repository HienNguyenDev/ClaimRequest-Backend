using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Projects;


namespace ClaimRequest.Application.Projects.CreateProjectMember
{
    public sealed class CreateProjectmemberCommand : ICommand<ProjectMember>
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public RoleInProject RoleInProject { get; set; }
    }
}
