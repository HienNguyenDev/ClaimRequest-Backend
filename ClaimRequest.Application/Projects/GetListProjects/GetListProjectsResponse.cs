using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Application.Projects.GetListProjects
{
    public sealed record GetListProjectsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
        public ProjectStatus Status { get; set; }
        public List<ProjectMember> Member { get; set; }
        
        public string CustomerName { get; set; }
    }
}
