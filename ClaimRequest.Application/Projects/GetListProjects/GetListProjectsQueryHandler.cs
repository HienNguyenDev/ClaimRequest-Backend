using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Application.Projects.GetProjectMembers;
using ClaimRequest.Domain.Common;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.Projects.GetListProjects
{
    public class GetListProjectsQueryHandler(IDbContext context)
        : IQueryHandler<GetListProjectsQuery, Page<GetListProjectsResponse>>
    {
        public async Task<Result<Page<GetListProjectsResponse>>> Handle(GetListProjectsQuery request,
            CancellationToken cancellationToken)
        {
            var project = context.Projects.Where(p => p.IsSoftDelete == 0);
            var result = await  project.ApplyPagination(
                request.PageNumber,
                request.PageSize).Select(x => new GetListProjectsResponse
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
                DepartmentId = x.DepartmentId,
                DepartmentName = x.Department.Name,
                CustomerName = x.CustomerName,
                Member = x.Members.Select(u => new ProjectMember()
                {
                    UserId = u.User.Id,
                    UserName = u.User.FullName,
                    Email = u.User.Email,
                    Role = u.RoleInProject.ToString(),
                }).ToList(),
            }).ToListAsync();
            
            return new Page<GetListProjectsResponse>
            (result,
                project.Count(),
                request.PageNumber,
                request.PageSize);
        }
    }
}