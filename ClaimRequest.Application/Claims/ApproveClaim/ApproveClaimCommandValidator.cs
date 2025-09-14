using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.ApproveClaim
{
    public class ApproveClaimCommandValidator : AbstractValidator<ApproveClaimCommand>
    {
        public ApproveClaimCommandValidator() 
        {
            RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("Claim ID is required");
        }
    }
}
