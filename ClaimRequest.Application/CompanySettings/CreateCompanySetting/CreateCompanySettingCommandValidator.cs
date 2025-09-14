using FluentValidation;

namespace ClaimRequest.Application.CompanySettings.CreateCompanySetting;

public class CreateCompanySettingCommandValidator : AbstractValidator<CreateCompanySettingCommand>
{
    public CreateCompanySettingCommandValidator()
    {
        RuleFor(command => command.LimitDayOff)
            .GreaterThanOrEqualTo(0).WithMessage("Limit day off must be a non-negative integer");

        RuleFor(command => command.WorkStartTime)
            .NotNull().WithMessage("Work start time cannot be null");

        RuleFor(command => command.WorkEndTime)
            .NotNull().WithMessage("Work end time cannot be null");

        RuleFor(command => command)
            .Must(command => command.WorkStartTime < command.WorkEndTime)
            .WithMessage("Work start time must be earlier than work end time");
    }
}