using FluentValidation;


namespace ClaimRequest.Application.ReasonApplication.CreateReason
{
    public class CreateReasonCommandValidator : AbstractValidator<CreateReasonCommand>
    {
      public  CreateReasonCommandValidator() {
            RuleFor(command => command.RequestTypeId).NotNull().NotEmpty();
            RuleFor(command => command.Name).NotNull().NotEmpty();
           
            
        }
    }
}
