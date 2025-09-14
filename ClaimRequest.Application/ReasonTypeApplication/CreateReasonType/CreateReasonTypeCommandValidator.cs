using FluentValidation;


namespace ClaimRequest.Application.ReasonTypeApplication.CreateReasonType
{
    public class CreateReasonTypeCommandValidator : AbstractValidator<CreateReasonTypeCommand>
    {
        public CreateReasonTypeCommandValidator()
        {
            RuleFor(command => command.Name).NotNull().NotEmpty();
          
           
        }
    }
}
