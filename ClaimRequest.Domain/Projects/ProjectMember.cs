using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.Projects;

public class ProjectMember : Entity{
    public Guid Id { get; set; }
    public Guid ProjectID { get; set; }
    public Guid UserID { get; set; }
    public RoleInProject RoleInProject { get; set; }
    
    // navigation property
    public virtual Project Project { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}