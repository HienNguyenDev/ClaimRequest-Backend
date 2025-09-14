using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Apis.Requests
{
    public class UpdateProjectRequest
    {
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus? Status { get; set; }
    }
}
