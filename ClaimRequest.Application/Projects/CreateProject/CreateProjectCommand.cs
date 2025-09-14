
using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Projects.CreateProject
{
    public sealed class CreateProjectCommand : ICommand<CreateProjectCommandResponse>
    {
        public string Name { get; init; }
        public string Code { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public string Description { get; init; }
        
        public Guid DepartmentId { get; init; }
        public string CustomerName { get; init; }
    }

}
