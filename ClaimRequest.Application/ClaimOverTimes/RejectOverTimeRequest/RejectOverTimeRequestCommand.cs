using ClaimRequest.Application.Abstraction.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClaimRequest.Application.ClaimOverTimes.RejectOverTimeRequest
{
    public class RejectOverTimeRequestCommand : ICommand<string>
    {
        public Guid Id { get; set; }
    }
}
