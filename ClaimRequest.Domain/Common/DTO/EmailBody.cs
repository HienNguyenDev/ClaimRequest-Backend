using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.Common.DTO;

public class EmailBody
{
    public User User { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public string MainContent { get; set; }
    
    
}