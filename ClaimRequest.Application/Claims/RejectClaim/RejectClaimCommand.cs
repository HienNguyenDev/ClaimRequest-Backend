using ClaimRequest.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClaimRequest.Application.Claims.RejectClaim
{
    public sealed class RejectClaimCommand : ICommand<string>
    {
       public Guid Id { get; set; }
    }
}
