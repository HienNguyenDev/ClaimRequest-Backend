using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.GetUserBuLeader;

public class GetUserBuLeaderQueryHandler
        (IDbContext dbContext,
        IUserContext userContext ): IQueryHandler<GetUserBuLeaderQuery, GetUserBuLeaderQueryItem>
{
    public async Task<Result<GetUserBuLeaderQueryItem>> Handle(GetUserBuLeaderQuery request, CancellationToken cancellationToken)
    {
        var userId = userContext.UserId;
        
        var user = await dbContext.Users
            .SingleOrDefaultAsync(u => u.Id == userId);
        
        var leader = await  dbContext.Users
            .SingleOrDefaultAsync(u => u.DepartmentId == user.DepartmentId && u.Role == UserRole.BULeader);
        
        
        return new GetUserBuLeaderQueryItem
        {
            Id = leader.Id,
            FullName = leader.FullName,
        };

    }
}