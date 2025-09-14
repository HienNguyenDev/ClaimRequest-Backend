using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.AttendanceRecords;

public class AttendanceRecord : Entity
{
    public Guid Id { get; set; }  
    public Guid UserId { get; set; }  
    public DateTime? CheckInTime { get; set; } 
    public DateTime? CheckOutTime { get; set; }  
    public DateOnly WorkDate { get; set; }  
    /*
    public WorkStatus? WorkStatus { get; set; }
    */
                                                                                                  
    public bool IsLateCome { get; set; }
    public bool IsLeaveEarly  { get; set; }
    
    public User User { get; set; } = null!;
    /*
    public virtual ClaimDetail? ClaimDetail { get; set; }
*/
}