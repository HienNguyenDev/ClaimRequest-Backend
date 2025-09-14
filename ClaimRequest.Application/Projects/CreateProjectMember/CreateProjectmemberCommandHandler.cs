using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Projects.Events;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Projects.CreateProjectMember
{
    public class CreateProjectmemberCommandHandler(IDbContext context) : ICommandHandler<CreateProjectmemberCommand, ProjectMember>
    {
        public async Task<Result<ProjectMember>> Handle(CreateProjectmemberCommand command, CancellationToken cancellationToken)
        {
            // Kiểm tra xem projectmember đã tồn tại
            if (await context.ProjectMembers.AnyAsync(pm => pm.UserID == command.UserID && pm.ProjectID == command.ProjectID, cancellationToken))
            {
                return Result.Failure<ProjectMember>(ProjectmemberErrors.MemberAlreadyExists);
            }

            // Kiểm tra xem dự án có tồn tại không
            var projectExists = await context.Projects.AnyAsync(p => p.Id == command.ProjectID, cancellationToken);
            if (!projectExists)
            {
                return Result.Failure<ProjectMember>(ProjectmemberErrors.NotFound(command.ProjectID));
            }

            // Kiểm tra xem người dùng có tồn tại không
            var userExists = await context.Users.AnyAsync(u => u.Id == command.UserID, cancellationToken);
            if (!userExists)
            {
                return Result.Failure<ProjectMember>(ProjectmemberErrors.NotFound(command.UserID));
            }

            // Kiểm tra vai trò có hợp lệ không
            if (!Enum.IsDefined(typeof(RoleInProject), command.RoleInProject))
            {
                return Result.Failure<ProjectMember>(ProjectmemberErrors.RoleOutOfRange);
            }

            var projectMember = new ProjectMember()
            {
                Id = Guid.NewGuid(),
                ProjectID = command.ProjectID,
                UserID = command.UserID,
                RoleInProject = command.RoleInProject,
            };

            projectMember.Raise(new ProjectmemberCreatedDomainEvent(projectMember.Id));

            context.ProjectMembers.Add(projectMember);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(projectMember);
        }
    }
}
