using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.ApproveClaim
{
    public  class ApproveClaimCommand : ICommand<string>
    {
        public Guid Id { get; set; }
    }
}
