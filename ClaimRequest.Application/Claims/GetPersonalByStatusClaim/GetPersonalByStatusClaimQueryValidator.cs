using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Users.CreateUser;
using FluentValidation;

namespace ClaimRequest.Application.Claims.GetPersonalByStatusClaim
{
    public class GetApprovedClaimQueryValidator : AbstractValidator<GetPersonalByStatusClaimQuery>
    {
        public GetApprovedClaimQueryValidator()
        {
            RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
            RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
        }
    }
}
