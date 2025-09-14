using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClaimRequest.Application.ClaimOverTimes.RejectOverTimeRequest
{
    public class RejectOverTimeRequestCommandValidator : AbstractValidator<RejectOverTimeRequestCommand>
    {  
        public RejectOverTimeRequestCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("OverTime Entry ID is required");
        }
    }
}
