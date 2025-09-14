using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Sites
{
    public class CreateSiteCommandValidator : AbstractValidator<CreateSiteCommand>
    {
        public CreateSiteCommandValidator() 
        {
            RuleFor(command => command.Name).NotEmpty().NotNull();
        }
    }
}
