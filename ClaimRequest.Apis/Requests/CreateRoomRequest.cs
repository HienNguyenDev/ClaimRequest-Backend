using ClaimRequest.Domain.SitesAndRooms;

namespace ClaimRequest.Apis.Requests
{
    public class CreateRoomRequest
    {
        public string Name { get; set; }
        public Guid SiteId { get; set; }
    }
}
