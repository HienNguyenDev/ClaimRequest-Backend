using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Projects.Errors;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.Projects.UpDateProject
{
    public class UpDateProjectCommandHandler : ICommandHandler<UpDateProjectCommand>
    {
        private readonly IDbContext _context;
        public UpDateProjectCommandHandler(IDbContext context)
        {
            _context = context;
        }
        public async Task<Result>Handle(UpDateProjectCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == command.ProjectId, cancellationToken);
                if (project == null)
                {
                    return Result.Failure(ProjectErrors.NotFound(command.ProjectId));
                }

                if (string.IsNullOrEmpty(command.Name) || command.Name.Length > 255)
                {
                    return Result.Failure(ProjectErrors.InvalidName);
                }

                var isCodeExists = await _context.Projects.AnyAsync(p => p.Code == command.Code && p.Id != command.ProjectId, cancellationToken);
                if (isCodeExists || string.IsNullOrEmpty(command.Code))
                {
                    return Result.Failure(ProjectErrors.CodeAlreadyExists);
                }

                if (command.StartDate > command.EndDate)
                {
                    return Result.Failure(ProjectErrors.InvalidDateRange);
                }

                if (command.Status != ProjectStatus.Finished && command.Status != ProjectStatus.InProgress)
                {
                    return Result.Failure(ProjectErrors.InvalidStatus);
                }

                if (command.IsSoftDelete != 0 && command.IsSoftDelete != 1)
                {
                    return Result.Failure(ProjectErrors.InvalidSoftDeleteValue);
                }
                
                project.Name =  command.Name ?? project.Name!;
                project.Code = command.Code ?? project.Code!;
                project.StartDate =  command.StartDate ?? project.StartDate;
                project.EndDate = command.EndDate ?? project.EndDate;
                project.Status = command.Status ?? project.Status;

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(project);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

