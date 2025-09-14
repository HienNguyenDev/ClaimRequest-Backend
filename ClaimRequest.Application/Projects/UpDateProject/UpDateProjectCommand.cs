using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Projects;

namespace ClaimRequest.Application.Projects.UpDateProject
{
    public sealed class UpDateProjectCommand : ICommand
    {
        public Guid ProjectId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ProjectStatus? Status { get; set; }
        public byte IsSoftDelete { get; set; }
    }
}
