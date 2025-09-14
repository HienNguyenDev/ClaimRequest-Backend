using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Users.ReActiveUser
{
    public class ReActiveUserCommandValidator : AbstractValidator<ReActiveUserCommand>
    {
        public ReActiveUserCommandValidator()
            {
                RuleFor(command => command.Email).NotNull().NotEmpty();
            }
    }

}
