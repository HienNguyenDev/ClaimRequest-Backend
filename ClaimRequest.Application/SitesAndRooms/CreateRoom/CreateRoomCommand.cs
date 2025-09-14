using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.SitesAndRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.SitesAndRooms.CreateRoom
{
    public sealed class CreateRoomCommand : ICommand<Room>
    {
        public string Name { get; set; }
        public Guid SiteId { get; set; }
    }
}
