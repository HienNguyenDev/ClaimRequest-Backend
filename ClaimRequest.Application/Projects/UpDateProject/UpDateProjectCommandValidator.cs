using FluentValidation;

namespace ClaimRequest.Application.Projects.UpDateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpDateProjectCommand>
    {
        /*public UpdateProjectCommandValidator()
        {

            RuleFor(command => command.projectId).NotNull().NotEmpty().WithMessage("Project ID is required.");
            RuleFor(command => command.Name).NotNull().NotEmpty().WithMessage("Name is required.");
            RuleFor(command => command.Code).NotNull().NotEmpty().WithMessage("Code is required.");
            RuleFor(command => command.StartDate).NotNull().NotEmpty().WithMessage("Start Date is required.");
            RuleFor(command => command.EndDate).NotNull().NotEmpty().WithMessage("End Date is required.");
            RuleFor(command => command.Status).NotNull().NotEmpty().WithMessage("Status is required.");
            RuleFor(command => command.IsSoftDelete).NotNull().WithMessage("IsSoftDelete is required.");
        }*/

    }
}
