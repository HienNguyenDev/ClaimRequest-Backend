namespace ClaimRequest.Apis.Requests;

public class CreateOverTimeRequest
{
    public Guid ProjectId { get; set; }
    public Guid ApproverId { get; set; }
        
    public List<DateOnly> OverTimeDates { get; set; }
        
    public List<Guid> OverTimeMembersIds { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public Guid RoomId { get; set; }
    
    public bool HasWeekend { get; set; }
    public bool HasWeekday { get; set; }
}