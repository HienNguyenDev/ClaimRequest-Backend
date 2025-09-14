using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Application.Projects.CreateProject;

public sealed record CreateProjectCommandResponse
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; } = null!;
    public byte IsSoftDelete { get; set; }
    public ProjectStatus Status { get; set; }
    
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
    public string CustomerName { get; set; } = null!;
}
