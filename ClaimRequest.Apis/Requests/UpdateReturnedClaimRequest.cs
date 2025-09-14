using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Apis.Requests
{
    public class UpdateReturnedClaimRequest
    {
        public Guid ClaimId { get; set; }
        public Guid ReasonId { get; set; }
        public string? OtherReasonText { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<DateOnly> DatesForClaim { get; set; } = new List<DateOnly>();
        public Guid SupervisorId { get; set; }
        public Guid ApproverId { get; set; }

        public Partial Partial { get; set; }
        public DateOnly ExpectedApproveDay { get; set; }

        public List<Guid> InformTos { get; set; } = new List<Guid>();
    }
}
