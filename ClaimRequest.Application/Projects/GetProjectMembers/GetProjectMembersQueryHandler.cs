using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Projects.GetProjectMembers;

public class GetProjectMembersQueryHandler(IDbContext context) : IQueryHandler<GetProjectMembersQuery, List<GetProjectMembersResponse>>
{
    public async Task<Result<List<GetProjectMembersResponse>>> Handle(GetProjectMembersQuery request, CancellationToken cancellationToken)
    {
        var project = await context.Projects
            .Include(p => p.Members)
            .ThenInclude(m => m.User)
            .FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

        if (project == null)
        {
            return Result.Failure<List<GetProjectMembersResponse>>(ProjectErrors.NotFound(request.ProjectId));
        }

        //Get members
        var members = project.Members.Select(m => new GetProjectMembersResponse
        {
            UserId = m.UserID,
            UserName = m.User.FullName,
            Email = m.User.Email,
            Role = m.RoleInProject.ToString(),
            NumberOfOvertimeHours = 0,
        }).ToList();

        var userIds = members
            .Select(m => m.UserId).ToList();
            
        var thisMonthYear = new  DateOnly(DateTime.Now.Year, DateTime.Now.Month,1);
            
        Dictionary<Guid, int> userIdsWithHours = context.Users.Where(u => userIds.Contains(u.Id))
            .Select(u => new
            {
                Id = u.Id,
                Number = u.SalaryPerMonths.Single(s => s.MonthYear == thisMonthYear).OvertimeHours,
            }).ToDictionary(u => u.Id, u => u.Number);

        foreach (var member in members)
        {
            if (userIdsWithHours.TryGetValue(member.UserId, out var hours))
            {
                member.NumberOfOvertimeHours = hours;
            }
        }
        
        return Result.Success(members);
    }
}