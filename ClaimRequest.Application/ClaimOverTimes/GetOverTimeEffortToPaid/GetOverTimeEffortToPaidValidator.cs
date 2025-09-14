
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToPaid;
using FluentValidation;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;

public class GetOverTimeEffortToPaidValidator : AbstractValidator<GetOverTimeEffortToPaidQuery>
{
    public GetOverTimeEffortToPaidValidator()
    {
        RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
        RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
    }
}