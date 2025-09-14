using ClaimRequest.Domain.ClaimOverTime;

namespace ClaimRequest.Application.ClaimOverTimes.GetListOvertimeRequestCurrentUserCreated;

public class GetListOvertimeRequestCurrentUserCreatedQueryResponse
{
    public Guid OverTimeRequestId { get; set; }
    public string ProjectManagerName { get; set; }
    public string ProjectName { get; set; }
    public DateTime CreatedDate { get; set; }
    public OverTimeRequestStatus Status { get; set; }
    public string ApproverName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string RoomName { get; set; }
    public string SiteName { get; set; }
    public bool HasWeekend { get; set; }
    public bool HasWeekday { get; set; }
}