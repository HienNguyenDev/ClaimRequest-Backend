using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.LateEarlyCases;

public class LateEarlyCase
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CheckInTime { get; set; }
    public DateTime CheckoutTime { get; set; }
    public bool IsLateCome { get; set; }
    public bool IsEarlyLeave { get; set; }
    public DateOnly WorkDate { get; set; }  
    public TimeSpan LateTimeSpan { get; set; }
    public TimeSpan EarlyTimeSpan { get; set; }
    
    //navigation property 
    /*
    public ClaimDetail? ClaimDetail { get; set; }
    */
    public ICollection<ClaimDetail> ClaimDetails { get; set; } = new List<ClaimDetail>();
    
    public User User { get; set; }
}