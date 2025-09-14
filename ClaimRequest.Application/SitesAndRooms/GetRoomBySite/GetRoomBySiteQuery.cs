using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.SitesAndRooms.GetRoomBySite
{
    public class GetRoomBySiteQuery : IQuery<List<GetRoomBySiteItem>>
    {
        public Guid SiteId { get; set; }
        public GetRoomBySiteQuery(Guid siteId)
        {
            SiteId = siteId;
        }
    }
    public class GetRoomBySiteItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
