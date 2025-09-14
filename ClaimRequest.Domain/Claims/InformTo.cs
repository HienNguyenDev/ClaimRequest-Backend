using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.Claims;

public class InformTo : Entity
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }
    
    //navigation property
    public virtual Claim Claim { get; set; }
    public virtual User User { get; set; }
}