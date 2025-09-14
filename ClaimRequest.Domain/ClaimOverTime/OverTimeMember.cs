using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.ClaimOverTime;

public class OverTimeMember : Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RequestId { get; set; }
    public virtual OverTimeRequest Request { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    
    public ICollection<OverTimeEffort> OverTimeEfforts { get; set; } = new List<OverTimeEffort>();
}