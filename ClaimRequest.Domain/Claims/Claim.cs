using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using ClaimRequest.Domain.Reasons;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.Claims;

public class Claim : Entity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ReasonId { get; set; }
    public string? OtherReasonText { get; set; }
    public ClaimStatus Status { get; set; }
    public byte IsSoftDeleted { get; set; }
    public Guid SupervisorId { get; set; }
    public Guid ApproverId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    public decimal? ClaimFee { get; set; }
    public Partial Partial { get; set; }
    
    public DateOnly ExpectApproveDay { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    //navigation property
    public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    public virtual User User { get; set; } = null!;
    public virtual Reason Reason { get; set; } = null!;
    public virtual User Supervisor { get; set; } = null!;
    public virtual User Approver { get; set; } = null!;
    public ICollection<ClaimDetail> ClaimDetails { get; set; } = new List<ClaimDetail>();
    
    public ICollection<InformTo> InformTos { get; set; } = new List<InformTo>();
    
}