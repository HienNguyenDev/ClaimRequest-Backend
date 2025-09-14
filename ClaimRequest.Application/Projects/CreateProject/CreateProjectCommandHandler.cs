using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Projects.Events;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;
using Department = ClaimRequest.Domain.Users.Department;

namespace ClaimRequest.Application.Projects.CreateProject
{
    public class CreateProjectCommandHandler(IDbContext context) : ICommandHandler<CreateProjectCommand, CreateProjectCommandResponse>
    {
        public async Task<Result<CreateProjectCommandResponse>> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
        {
            if (await context.Projects.AnyAsync(p => p.Code == command.Code, cancellationToken))
            {
                return Result.Failure<CreateProjectCommandResponse>(ProjectErrors.CodeNotUnique);
            }
            
            Department? department = await context.Departments.FindAsync(command.DepartmentId);
            
            if (department == null)
            {
                return Result.Failure<CreateProjectCommandResponse>(DepartmentErrors.NotFound(command.DepartmentId));
            }
            
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = command.Name.Trim(),
                Code = command.Code.Trim(),
                StartDate = command.StartDate,
                EndDate = command.EndDate,
                Description = command.Description,
                Status = ProjectStatus.InProgress,
                DepartmentId = command.DepartmentId,
                IsSoftDelete = 0,
                CustomerName = command.CustomerName.Trim(),
            };

            project.Raise(new ProjectCreatedDomainEvent(project.Id));
            context.Projects.Add(project);
            await context.SaveChangesAsync(cancellationToken);

            var projectResponse = new CreateProjectCommandResponse
            {
                Name = project.Name,
                Code = project.Code,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Description = project.Description,
                Status = project.Status,
                IsSoftDelete = project.IsSoftDelete,
                DepartmentId = project.DepartmentId,
                DepartmentName = department!.Name,
            };

            return projectResponse;
        }
    }
}

