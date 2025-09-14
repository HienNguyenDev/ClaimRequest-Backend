using FluentValidation;

namespace ClaimRequest.Application.Projects.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotNull().NotEmpty().WithMessage("Name cannot be blank")
                .MaximumLength(255).WithMessage("Name cannot be longer than 255 characters");

            RuleFor(command => command.Code)
                .NotNull().NotEmpty().WithMessage("Code cannot be blank")
                .MaximumLength(50).WithMessage("Code cannot be longer than 50 characters");

            RuleFor(command => command.StartDate)
                .LessThan(command => command.EndDate).WithMessage("Start Date must be earlier than End Date")
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("Start Date must be later than current date.");

            RuleFor(command => command.EndDate)
                .GreaterThan(command => command.StartDate).WithMessage("End Date must be later than Start date");

            RuleFor(command => command.Description).NotNull().NotEmpty().WithMessage("Code cannot be blank");
            RuleFor(command => command.DepartmentId).NotNull().NotEmpty().WithMessage("A project should be managed by one department");
            
            RuleFor(command => command.Name)
                .NotNull().NotEmpty().WithMessage("Customer Name cannot be blank")
                .MaximumLength(255).WithMessage("Customer Name cannot be longer than 255 characters");
        }
    }
}

