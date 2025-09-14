using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortByOTMember
{
    public class GetOverTimeEffortByOTMemberValidator: AbstractValidator<GetOverTimeEffortByOTMemberQuery>
    {
        public GetOverTimeEffortByOTMemberValidator()
        {
            RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
            RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
        }
    }
}
