using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Projects.GetProjectDetails;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Users;
using ClaimRequest.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.GetCurrentUser
{
    public class GetCurrentUserQueryHandler(IDbContext context, IUserContext userContext) : IQueryHandler<GetCurrentUserQuery, GetCurrentUserResponse>
    {
        public async Task<Result<GetCurrentUserResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = userContext.UserId;

            var currentUser = await context.Users.Where(p => p.Id == currentUserId).Select(p => new GetCurrentUserResponse{
                FullName = p.FullName,
                Email = p.Email,
                Role = p.Role.ToString(),
                Rank = p.Rank.ToString(),
                DepartmentName = p.Departments.Name,
                ProjectsName = p.ProjectMembers.Select(up => up.Project.Name).ToList()
            }).FirstOrDefaultAsync();   

            return currentUser;
        }
    }
}
