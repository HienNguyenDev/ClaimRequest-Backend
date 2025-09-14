using FluentValidation;

namespace ClaimRequest.Application.Claims.CreateClaim;

public class CreateClaimCommandValidator : AbstractValidator<CreateClaimCommand>
{
    public CreateClaimCommandValidator()
    {

        RuleFor(command => command.ApproverId).NotEmpty().NotNull();
        RuleFor(command => command.SupervisorId).NotEmpty().NotNull();
        
        RuleFor(command => command.ExpectApproveDay).NotNull().NotEmpty();
        RuleFor(command => command.ReasonId).NotEmpty().NotNull();
       
        RuleFor(command => command.Partial).IsInEnum().NotEmpty().NotNull();
        RuleFor(command => command.DatesForClaim).NotNull().NotEmpty();
        
    }
}