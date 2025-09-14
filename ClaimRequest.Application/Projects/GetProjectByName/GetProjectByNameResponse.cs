using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Application.Projects.GetProjectByName;

public class  GetProjectByNameResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
}