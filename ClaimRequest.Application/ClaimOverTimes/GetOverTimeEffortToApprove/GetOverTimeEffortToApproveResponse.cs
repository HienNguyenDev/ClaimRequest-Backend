using ClaimRequest.Domain.ClaimOverTime;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;

public class GetOverTimeEffortToApproveResponse
{
    public Guid Id { get; set; }
    public Guid OverTimeMemberId { get; set; }
    public Guid OverTimeDateId { get; set; }
    public int Hours { get; set; }
    public int DayHours { get; set; }
    public int NightHours { get; set; }
    public string TaskDescription { get; set; } = null!;
    public OverTimeEffortStatus Status { get; set; }
    public OverTimeMemberResponse? OverTimeMember { get; set; }
    public OverTimeDateResponse? OverTimeDate { get; set; }
    
    public OverTimeRequestResponse? OverTimeRequest { get; set; }
}
public class OverTimeMemberResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = null!;
    public Guid RequestId { get; set; }
}

public class OverTimeDateResponse
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public Guid OverTimeRequestId { get; set; }
}
public class OverTimeRequestResponse
{
    public Guid Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public OverTimeRequestStatus Status { get; set; }
}

