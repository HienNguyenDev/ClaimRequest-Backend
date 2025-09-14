using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.Projects;


public class Project : Entity 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = null!;
    public Guid DepartmentId { get; set; }
    public byte IsSoftDelete { get; set; }
    public ProjectStatus Status { get; set; }
    
    public string CustomerName { get; set; } = null!;
    
    //navigation property
    
    public Department Department { get; set; } = null!;
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    public ICollection<OverTimeRequest> OvertimeRequests { get; set; } = new List<OverTimeRequest>();

}