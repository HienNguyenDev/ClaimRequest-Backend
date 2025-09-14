using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.ClaimOverTime;

public class OverTimeDate : Entity
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid OverTimeRequestId { get; set; }
    public virtual OverTimeRequest OverTimeRequest { get; set; } = null!;
    public ICollection<OverTimeEffort> OverTimeEfforts { get; set; } = new List<OverTimeEffort>();
}