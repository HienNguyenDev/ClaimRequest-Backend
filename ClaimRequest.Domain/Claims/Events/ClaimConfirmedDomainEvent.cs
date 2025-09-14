using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Claims.Events
{
    public sealed record ClaimConfirmedDomainEvent : IDomainEvent
    { 
        public Guid ClaimId { get; set; }
        public string Action { get; set; } = null!;
        public Guid UserId { get; set; }
        public DateTime PerformedAt { get; set; }
    }
}
