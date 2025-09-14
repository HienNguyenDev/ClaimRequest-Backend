using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Users.SearchUserByEmail;
public class SearchUserByEmailQuery : IQuery<SearchUserByEmailItem>
{
    public string? Email { get; set; }
    public SearchUserByEmailQuery (string email)
    {
        Email = email;
    }
}
public class SearchUserByEmailItem
{
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int NumberOfOvertimeHours { get; set; }
}

