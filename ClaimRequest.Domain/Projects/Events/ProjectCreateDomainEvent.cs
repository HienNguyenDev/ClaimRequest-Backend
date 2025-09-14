using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Projects.Events
{
    public sealed record ProjectCreatedDomainEvent(Guid Id) : IDomainEvent;
}