using ClaimRequest.Application.Abstraction.Messaging;


namespace ClaimRequest.Application.Projects.GetProjectByName;

public sealed class GetProjectByNameQuery : IQuery<List<GetProjectByNameResponse>>
{
    public string Name { get; set; }

    public GetProjectByNameQuery(string name)
    {
        Name = name;
    }
}