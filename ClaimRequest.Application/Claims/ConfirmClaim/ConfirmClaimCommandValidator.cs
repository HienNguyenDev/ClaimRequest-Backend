using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Users.CreateUser;
using FluentValidation;

namespace ClaimRequest.Application.Claims.ConfirmClaim
{
    public class ConfirmClaimCommandValidator : AbstractValidator<ConfirmClaimCommand>
    {
        public ConfirmClaimCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("Claim ID is required");
        }
    }
}
