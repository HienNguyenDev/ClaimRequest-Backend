using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Domain.Reasons;

public class Reason 
{
    public Guid Id { get; set; }
    public Guid RequestTypeId { get; set; }
    public string Name { get; set; } = null!;
    public bool IsOther { get; set; }
    public byte IsSoftDeleted { get; set; }
    
    //navigation property
    public virtual ReasonType ReasonType { get; set; } = null!;
    public ICollection<Claim> Claims { get; set; } = new List<Claim>();

}