using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Claims.Events
{
    public sealed record ClaimUpdatedDomainEvent(Guid ClaimId) : IDomainEvent
    {
    }
}
