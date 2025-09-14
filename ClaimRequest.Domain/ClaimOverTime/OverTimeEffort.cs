using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.ClaimOverTime;

public class OverTimeEffort : Entity
{
    public Guid Id { get; set; }
    public Guid OverTimeMemberId { get; set; }
    public Guid OverTimeDateId { get; set; }
    public int DayHours { get; set; }
    public int NightHours { get; set; }
    public string TaskDescription { get; set; } = null!;
    public OverTimeEffortStatus Status { get; set; }
    public virtual OverTimeMember OverTimeMember { get; set; } = null!;
    public virtual OverTimeDate OverTimeDate { get; set; } = null!;
}