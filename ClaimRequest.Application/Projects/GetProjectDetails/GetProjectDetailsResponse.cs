using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Application.Projects.GetProjectDetails
{
    public sealed record GetProjectDetailsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = null;
        public byte IsSoftDelete { get; set; }
        public ProjectStatus Status { get; set; }
    }
}
