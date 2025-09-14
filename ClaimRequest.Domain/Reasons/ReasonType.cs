using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Reasons;

public class ReasonType : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public byte IsSoftDeleted { get; set; }
    // navigation property
    public ICollection<Reason> Reasons { get; set; } = new List<Reason>();
}