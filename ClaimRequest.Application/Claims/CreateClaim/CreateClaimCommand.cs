using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Application.Claims.CreateClaim;

public class CreateClaimCommand : ICommand
{ 
    public Guid ReasonId { get; set; }
    public string? OtherReasonText { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public List<DateOnly> DatesForClaim { get; set; } = new List<DateOnly>();
    public Guid SupervisorId { get; set; }
    public Guid ApproverId { get; set; }
    
    public Partial Partial { get; set; }
    
    public DateOnly ExpectApproveDay { get; set; }

    public decimal? ClaimFee { get; set; }
    public List<Guid> InformTos { get; set; } = new List<Guid>();

}

