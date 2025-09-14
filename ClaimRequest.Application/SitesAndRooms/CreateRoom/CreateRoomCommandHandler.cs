using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonApplication.GetReasons;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.SitesAndRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.SitesAndRooms.CreateRoom
{
    public class CreateRoomCommandHandler(IDbContext context) : ICommandHandler<CreateRoomCommand, Room>
    {
        public async Task<Result<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = new Room()
            {
                Id = Guid.NewGuid(),
                SiteId = request.SiteId,
                Name = request.Name,
            };
            //reason.Raise(new ReasonCreatedDomainEvent(reason.Id));
            context.Rooms.Add(room);

            await context.SaveChangesAsync(cancellationToken);

            return new Room
            {
                Id = room.Id,
                SiteId = room.SiteId,
                Name = room.Name,

            };
        }
    }
}
