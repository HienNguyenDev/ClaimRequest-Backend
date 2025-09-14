using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.Projects.RemoveUserFromProject;

public class RemoveUserFromProjectCommand : ICommand<Result>
{
    public Guid ProjectID { get; set; }
    public Guid UserID { get; set; }

    public RemoveUserFromProjectCommand(Guid projectID, Guid userID)
    {
        ProjectID = projectID;
        UserID = userID;
    }
}

