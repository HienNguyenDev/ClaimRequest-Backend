using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Domain.Users;

public class Department : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    //navigation property
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}

