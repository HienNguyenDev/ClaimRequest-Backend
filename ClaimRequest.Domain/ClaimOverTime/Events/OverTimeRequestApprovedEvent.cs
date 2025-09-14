using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.ClaimOverTime.Events
{
    public sealed record class OverTimeRequestApprovedEvent : IDomainEvent
    {
        public Guid RequestId { get; set; }
        //public Guid ApproverId { get; set; }
    }
}
