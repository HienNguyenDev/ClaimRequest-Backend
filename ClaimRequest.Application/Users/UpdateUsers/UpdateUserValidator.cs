using FluentValidation;

namespace ClaimRequest.Application.Users.UpdateUsers;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        
        RuleFor(command => command.FullName)
            .MaximumLength(255).WithMessage("Full Name cannot exceed 255 characters.");

        /*RuleFor(command => command.Email)
            .EmailAddress().WithMessage("Invalid email format.");*/

        RuleFor(command => command.Role)
            .IsInEnum().WithMessage("Invalid Role value.");

        RuleFor(command => command.Rank)
            .IsInEnum().WithMessage("Invalid Rank value.");

        RuleFor(command => command.BaseSalary)
            .GreaterThanOrEqualTo(0).WithMessage("Base Salary must be a positive number.");

        RuleFor(command => command.Status)
            .IsInEnum().WithMessage("Invalid Status value.");
    }
}