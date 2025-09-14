using FluentValidation;


namespace ClaimRequest.Application.Projects.CreateProjectMember
{
    public class CreateProjectmemberCommandValidator : AbstractValidator<CreateProjectmemberCommand>
    {
        public CreateProjectmemberCommandValidator()
        { 
            RuleFor(command => command.UserID).NotNull().NotEmpty().WithMessage("User ID is required.");
            RuleFor(command => command.ProjectID).NotNull().NotEmpty().WithMessage("Project ID is required.");
            RuleFor(command => command.RoleInProject).NotNull().NotEmpty().WithMessage("Role must be a valid value.");
        }
    }
}
