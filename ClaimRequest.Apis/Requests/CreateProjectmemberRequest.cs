using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Apis.Requests
{
    public class CreateProjectmemberRequest
    {
        public Guid ProjectID { get; set; }
        public Guid UserID { get; set; }
        public RoleInProject RoleInProject { get; set; }
    }
}
