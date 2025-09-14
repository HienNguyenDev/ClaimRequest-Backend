using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.SitesAndRooms;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.ClaimOverTime;

public class OverTimeRequest : Entity
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ProjectManagerId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OverTimeRequestStatus Status { get; set; }
    public Guid ApproverId { get; set; }
    public DateOnly StartDate{ get; set; }
    public DateOnly EndDate{ get; set; }
    
    public Guid RoomId { get; set; }
    
    public bool HasWeekend { get; set; }
    public bool HasWeekday { get; set; }
    public virtual Room Room { get; set; }
    public virtual Project Project { get; set; } = null!;
    public virtual User Approver { get; set; } = null!;
    public virtual User CreatedByUser { get; set; } = null!;
 
    public ICollection<OverTimeMember> OverTimeMembers { get; set; } = new List<OverTimeMember>();
    
    public ICollection<OverTimeDate> OverTimeDates { get; set; } = new List<OverTimeDate>();
}