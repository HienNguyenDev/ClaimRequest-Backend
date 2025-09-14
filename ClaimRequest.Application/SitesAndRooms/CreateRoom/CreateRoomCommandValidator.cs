using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.SitesAndRooms.CreateRoom
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(command => command.Name).NotNull().NotEmpty();
            RuleFor(command => command.SiteId).NotNull().NotEmpty();

        }
    }
}
