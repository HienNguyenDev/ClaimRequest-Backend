using ClaimRequest.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.PaidClaim
{
    public sealed record PaidClaimCommand : ICommand
    {
        public Guid Id { get; set; }
        public PaidClaimCommand(Guid id)
        {
            Id = id;
        }
    }
}
