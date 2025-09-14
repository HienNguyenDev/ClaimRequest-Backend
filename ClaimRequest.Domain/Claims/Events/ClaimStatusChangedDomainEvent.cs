using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Claims.Events
{
    public sealed record ClaimStatusChangedDomainEvent : IDomainEvent
    {
        
        public Guid UserId { get; set;  }
        public Guid ClaimId { get; set; }
        //public string ReasonName { get; set; }
        //public string SupervisorName { get; set; }
        //public string ApproverName { get; set; }
        //public DateOnly StartDate { get; set; }
        //public DateOnly EndDate { get; set; } 
        //public decimal? ClaimFee { get; set; }
        public string Action { get; set; } = null!;
        public string UserActionName {  get; set; }
    }
}