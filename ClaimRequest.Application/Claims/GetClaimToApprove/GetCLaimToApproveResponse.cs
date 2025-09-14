using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Application.Claims.GetClaimToApprove
{
    public class GetCLaimToApproveResponse
    {
        public Guid Id { get; set; }
       public string? UserName { get; set; }
        public Guid ReasonId { get; set; }
        public string? ReasonName { get; set; }

        public Guid ReasonTypeId { get; set; }
        public string? ReasonTypeName { get; set; }
        public string? OtherReasonText { get; set; }
        public string? Remark { get; set; }
        public ClaimStatus Status { get; set; }
        public Guid? SupervisorId { get; set; }
        public string? SupervisorFullName { get; set; }
        public Guid ApproverId { get; set; }
        public string? ApproverFullName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public Partial Partial { get; set; }

        public decimal? ClaimFee { get; set; }
        public DateOnly ExpectApproveDay { get; set; }


        public DateOnly[]? ClaimDates { get; set; } 

        public List<InformToResponse>? InformTos { get; set; }
     
        
    }
    public class InformToResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }

}
