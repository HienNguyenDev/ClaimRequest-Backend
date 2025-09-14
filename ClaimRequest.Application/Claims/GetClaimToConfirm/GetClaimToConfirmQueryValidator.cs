using FluentValidation;

namespace ClaimRequest.Application.Claims.GetClaimToConfirm
{
    public class GetClaimToConfirmQueryValidator : AbstractValidator<GetClaimToConfirmQuery>
    {
        public GetClaimToConfirmQueryValidator()
        {
            RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
            RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
        }
    }
}
