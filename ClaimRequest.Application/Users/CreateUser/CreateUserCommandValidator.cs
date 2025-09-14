using FluentValidation;

namespace ClaimRequest.Application.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.FullName).NotNull().NotEmpty();
        RuleFor(command => command.Email).NotNull().NotEmpty();
        RuleFor(command => command.DepartmentId).NotNull().NotEmpty();
        RuleFor(command => command.Role).IsInEnum().NotNull().NotEmpty();
        RuleFor(command => command.Rank).IsInEnum().NotNull();
        RuleFor(command => command.BaseSalary).NotNull().NotEmpty();
    }
}