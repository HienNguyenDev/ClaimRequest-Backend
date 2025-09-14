using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.Claims.CancelClaim
{
    public sealed record CancelClaimCommand : ICommand
    {
        public Guid Id { get; set; }
        public CancelClaimCommand(Guid id)
        {
            Id = id;
        }
    }
}
