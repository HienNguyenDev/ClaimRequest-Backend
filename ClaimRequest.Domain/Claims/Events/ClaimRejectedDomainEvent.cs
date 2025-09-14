using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Domain.Claims.Events
{
   public class ClaimRejectedDomainEvent : IDomainEvent
    {
        public Guid ClaimId { get; set; }
        public string Action { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
