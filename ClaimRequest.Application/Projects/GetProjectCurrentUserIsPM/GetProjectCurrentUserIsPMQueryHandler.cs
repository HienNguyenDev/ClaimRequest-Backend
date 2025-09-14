using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Projects.GetProjectDetails;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Projects.GetCurrentUserIsPM
{
    public class GetProjectCurrentUserIsPMQueryHandler : IQueryHandler<GetProjectCurrentUserIsPMQuery, List<GetProjectCurrentUserIsPMQueryResponse>>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetProjectCurrentUserIsPMQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<Result<List<GetProjectCurrentUserIsPMQueryResponse>>> Handle(GetProjectCurrentUserIsPMQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.UserId;

            var projectsManaged = await _dbContext.ProjectMembers
                .Where(p => p.UserID == userId && p.RoleInProject == RoleInProject.ProjectManager)
                .Include(p => p.Project)
                .Select(p => new GetProjectCurrentUserIsPMQueryResponse
                {
                    Id = p.Id,
                    ProjectID = p.ProjectID,
                    UserID = p.UserID,
                    RoleInProject = p.RoleInProject,
                    Name = p.Project.Name,
                    Code = p.Project.Code,
                    Description = p.Project.Description,
                    DepartmentName = p.Project.Department.Name,
                    DepartmentId = p.Project.DepartmentId,
                    CustomerName = p.Project.CustomerName,
                }).ToListAsync();
            if (!projectsManaged.Any())
            {
                return Result.Failure<List<GetProjectCurrentUserIsPMQueryResponse>>(ProjectmemberErrors.UserIsNotProjectManagerInAnyProject(userId));
            }
            return Result.Success(projectsManaged);
        }
    }
}
