using ClaimRequest.Application.Users.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(command => command.CurrentPassword).NotNull().NotEmpty();
            RuleFor(command => command.NewPassword).NotNull().NotEmpty();
        }
    }
}
