using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.RejectClaim
{
    public class RejectClaimCommandValidator : AbstractValidator<RejectClaimCommand>
    {
        public RejectClaimCommandValidator() 
        {
            RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("Claim ID is required");
        }
    }
}
