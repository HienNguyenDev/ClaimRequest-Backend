using FluentValidation;

namespace ClaimRequest.Application.Claims.GetClaimToApprove
{
    public class GetClaimToQueryValidator : AbstractValidator<GetClaimToApproveQuery>
    {
        public GetClaimToQueryValidator()
        {
            RuleFor(command => command.PageNumber).NotNull().NotEmpty().WithMessage("Page Number is required");
            RuleFor(command => command.PageSize).NotNull().NotEmpty().WithMessage("Page Size is required");
        }
    }
}
