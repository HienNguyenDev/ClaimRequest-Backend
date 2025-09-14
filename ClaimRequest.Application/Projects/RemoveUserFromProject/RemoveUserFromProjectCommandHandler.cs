using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Projects.RemoveUserFromProject;

public class RemoveUserFromProjectCommandHandler(IDbContext context) : ICommandHandler<RemoveUserFromProjectCommand, Result>
{
    public async Task<Result<Result>> Handle(RemoveUserFromProjectCommand command, CancellationToken cancellationToken)
    {
        var projectMember = await context.ProjectMembers
            .FirstOrDefaultAsync(pm => pm.ProjectID == command.ProjectID && pm.UserID == command.UserID, cancellationToken);
        
        if (projectMember == null)
        {
            return Result.Failure(ProjectmemberErrors.UserNotInProject(command.UserID));
        }
        context.ProjectMembers.Remove(projectMember);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}