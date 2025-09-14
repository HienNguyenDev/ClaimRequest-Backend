using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Domain.ClaimOverTime.Events
{
    public sealed record class OverTimeEffortStatusChangedEvent : IDomainEvent
    {
        public Guid EffortId { get; set; }
        public string Action { get; set; }
        
        public Guid UserId { get; set; }
    }

}