namespace ClaimRequest.Domain.SitesAndRooms;

public class Room
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SiteId { get; set; }
    
    public virtual Site Site { get; set; }
}