using FluentValidation;

namespace ClaimRequest.Application.Users.CreateDepartment;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(command => command.Name).NotNull().NotEmpty();
        RuleFor(command => command.Code).NotNull().NotEmpty();
        RuleFor(command => command.Description).NotNull().NotEmpty();
    }
}