using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Projects.GetProjectDetails
{
    public sealed class GetProjectDetailsQuery : IQuery<GetProjectDetailsResponse>
    {
        public Guid Id { get; }
        public GetProjectDetailsQuery(Guid id)
        {
            Id = id;
        }
    }
}
