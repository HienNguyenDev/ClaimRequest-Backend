
using FluentValidation;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;

public class GetOverTimeEffortToApproveValidator : AbstractValidator<GetOverTimeEffortToApproveQuery>
{
    public GetOverTimeEffortToApproveValidator()
    {
        RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
        RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
    }
}