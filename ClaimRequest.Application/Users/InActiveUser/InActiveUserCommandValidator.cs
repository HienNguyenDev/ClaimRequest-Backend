using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.InActiveUser
{
    public class InActiveUserCommandValidator : AbstractValidator<InActiveUserCommand>
    {
        public InActiveUserCommandValidator()
        {
            RuleFor(command => command.Email).NotNull().NotEmpty();
        }
    }
}
