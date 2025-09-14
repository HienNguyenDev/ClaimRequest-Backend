using FluentValidation;

namespace ClaimRequest.Application.Users.Login;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Email).NotNull().NotEmpty().WithMessage("Email is required");
        RuleFor(command => command.Password).NotNull().NotEmpty().WithMessage("Password is required");
    }
}