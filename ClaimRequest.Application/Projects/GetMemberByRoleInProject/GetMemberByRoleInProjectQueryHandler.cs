using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects.Errors;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Projects.GetMemberByRoleInProject;

public class GetMemberByRoleInProjectQueryHandler(IDbContext dbContext) : IQueryHandler<GetMemberByRoleInProjectQuery, List<GetMemberByRoleInProjectResponse>>

{
    public async Task<Result<List<GetMemberByRoleInProjectResponse>>> Handle(GetMemberByRoleInProjectQuery request, CancellationToken cancellationToken)
    {
        var project = await dbContext.Projects.Include(x => x.Members).ThenInclude(x=>x.User).FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (project == null)
        {
            return Result.Failure<List<GetMemberByRoleInProjectResponse>>(ProjectErrors.NotFound(request.Id));
        }

        var member = project.Members.Where(x => x.RoleInProject.ToString() == request.Role).Select(x => new GetMemberByRoleInProjectResponse()
        {
            UserId = x.UserID,
            UserName = x.User.FullName,
            Email = x.User.Email,
        }).ToList();

        

        return Result.Success(member);

    }
}