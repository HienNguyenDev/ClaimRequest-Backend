using FluentValidation;

namespace ClaimRequest.Application.ClaimOverTimes.GetOverTimeRequestToApprove;

public class GetOverTimeRequestToApproveValidator : AbstractValidator<GetOverTimeRequestToApproveQuery>
{
    public GetOverTimeRequestToApproveValidator()
    {
        RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
        RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
    }
}