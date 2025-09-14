using FluentValidation;


namespace ClaimRequest.Application.Claims.PaidClaim
{
    public class PaidClaimCommandValidator : AbstractValidator<PaidClaimCommand>
    {
        public PaidClaimCommandValidator()
        {
            RuleFor(command => command.Id).NotNull().NotEmpty().WithMessage("Claim ID is required.");
        }
    }
}
