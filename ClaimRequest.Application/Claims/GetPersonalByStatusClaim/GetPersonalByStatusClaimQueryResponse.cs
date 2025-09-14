using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Application.Claims.GetPersonalByStatusClaim
{
    public sealed record GetPersonalByStatusClaimQueryResponse
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public Guid ReasonId { get; set; }
        public Guid ReasonTypeId { get; set; }
        public string ReasonName { get; set; }
        public string ReasonTypeName { get; set; }

        public string? OtherReasonText { get; set; }
        public string? Remark { get; set; }
        public ClaimStatus Status { get; set; }
        public Guid? SupervisorId { get; set; }
        public string? SupervisorFullName { get; set; }
        public Guid ApproverId { get; set; }
        public string?  ApproverFullName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public Partial Partial { get; set; }

        public decimal? ClaimFee { get; set; }
        public DateOnly ExpectApproveDay { get; set; }
        public List<DateOnly> ClaimDates { get; set; } = new();
        public List<InformToResponse>? InformTos { get; set; } =new();
    }
}
