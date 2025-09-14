using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Users.GetUserBuLeader;

public class GetUserBuLeaderQuery : IQuery<GetUserBuLeaderQueryItem> 
{
    
}

public class GetUserBuLeaderQueryItem
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
}