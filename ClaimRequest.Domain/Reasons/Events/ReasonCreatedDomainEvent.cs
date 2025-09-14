using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Domain.Reasons.Events
{
    public sealed class  ReasonCreatedDomainEvent(Guid id) : IDomainEvent;
    
}
