namespace ClaimRequest.Application.Attendance.GetLateEarlyCase;

public class LateEarlyQueryItem
{
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckoutTime { get; set; }
    public bool IsLateCome { get; set; }
    public bool IsEarlyLeave { get; set; }
    
    public TimeSpan? LateTimeSpan { get; set; }
    public TimeSpan? EarlyTimeSpan { get; set; }
}