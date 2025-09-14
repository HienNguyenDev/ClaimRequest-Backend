using ClaimRequest.Domain.ClaimOverTime;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeRequestToApprove;

public class GetOverTimeRequestToApproveResponse
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public Guid ProjectManagerId { get; set; }
    public DateOnly ForDate { get; set; }
    public DateTime CreatedAt { get; set; } 
    public string? CreatedByUser { get; set; }
    public DateOnly StartDate{ get; set; }
    public DateOnly EndDate{ get; set; }
    public OverTimeRequestStatus Status { get; set; }
    public Guid ApproverId { get; set; }
    public string? ApproverFullName { get; set; }
    public Guid RoomId { get; set; }
    public string? RoomName { get; set; }
    public Guid SiteId { get; set; }
    public string? SiteName { get; set; }
    public bool HasWeekday { get; set; }
    public bool HasWeekend { get; set; }
    public List<OverTimeMemBerResponse>? OverTimeMemBer { get; set; }
    public List<OverTimeDateResponse>? OverTimeDate { get; set; }
}

public class OverTimeMemBerResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }
}

public class OverTimeDateResponse
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
}
