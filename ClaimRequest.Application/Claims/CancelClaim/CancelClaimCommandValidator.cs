using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.CancelClaim
{
    public class CancelClaimCommandValidator : AbstractValidator<CancelClaimCommand>
    {
        public CancelClaimCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("Claim ID is required.");
        }
    }
}
