using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.AuditLogs;

public class AuditLog : Entity
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public string Action { get; set; } = null!;
    public Guid UserId { get; set; }
    public DateTime PerformedAt { get; set; }
    
    //navigation property
    public virtual Claim Claim { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}