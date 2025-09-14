using FluentValidation;

namespace ClaimRequest.Application.ClaimOverTimes.CreateOverTimeEffort
{
    public class CreateOverTimeEffortCommandValidator : AbstractValidator<CreateOverTimeEffortCommand>
    {
        public CreateOverTimeEffortCommandValidator()
        {
            RuleFor(command => command.RequestId).NotEmpty().NotNull();
            RuleFor(command => command.OverTimeMemberId).NotEmpty().NotNull();
            RuleFor(command => command.OverTimeDateId).NotEmpty().NotNull();
            RuleFor(command => command.TaskDescription).NotEmpty().NotNull();
            RuleFor(command => command)
                .Must(cmd => cmd.DayHours > 0 || cmd.NightHours > 0)
                .WithMessage("Both DayHours and NightHours cannot be zero at the same time.");
        }
    }
}
