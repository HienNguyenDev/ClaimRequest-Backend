using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Domain.ClaimOverTime.Events
{
    public sealed record class OverTimeRequestRejectedEvent : IDomainEvent
    {
        public Guid RequestId { get; set; }
        //public Guid ApproverId { get; set; }
    }
}
