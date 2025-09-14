using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Apis.Requests
{
    public sealed class CreateProjectRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        
        public Guid DepartmentId { get; set; }
        public string CustomerName { get; set; }
    }

}
