using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Users;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.GetUserProjectManager;


public abstract class GetUserSupervisor
{
    public sealed class Query : IQuery<List<ResponseItem>>{

}
    
   
public  class GetUserProjectManagerHandler
        (IDbContext dbContext, 
        IUserContext userContext) : IQueryHandler<GetUserSupervisor.Query, List<ResponseItem>>
{
   public async Task<Result<List<ResponseItem>>> Handle(Query request, CancellationToken cancellationToken)
   {
      var userId = userContext.UserId;

      var projects = dbContext.Projects
          .Where(p => p.Members
              .Any(m => m.RoleInProject != RoleInProject.ProjectManager && m.UserID == userId))
          .ToList();
      
      var projectIds = projects.Select(p => p.Id).ToList();

      var projectManagers = dbContext.ProjectMembers
          .Include(m => m.User)
          .Where(m => projectIds
              .Contains(m.ProjectID) && m.RoleInProject == RoleInProject.ProjectManager).ToList();
      
      
      var user = await dbContext.Users
          .SingleOrDefaultAsync(u => u.Id == userId);
      
      var leader = await  dbContext.Users
          .SingleOrDefaultAsync(u => u.DepartmentId == user.DepartmentId && u.Role == UserRole.BULeader);

      var leaderResponse = new ResponseItem
      {
          Id = leader.Id,
          Name = leader.FullName,
      };
      
      var managers = projectManagers.Select(m => new ResponseItem
      {
          Id = m.UserID,
          Name = m.User.FullName
      }).DistinctBy(u => u.Id).ToList();
      
      managers.Add(leaderResponse);
      
      managers = managers.DistinctBy(u => u.Id).ToList();
      return managers;
   }
}


public sealed class  ResponseItem {
   public Guid Id { get; set; }
   public string Name { get; set; }
}
}
