using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.ClaimOverTime.Events
{
    public class OverTimeEffortPaidEvent : IDomainEvent
    {
        public Guid UserId { get; set; }
        public int Effort { get; set; }
    }
}
