namespace ClaimRequest.Domain.SitesAndRooms;

public class Site
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Room> Rooms { get; set; }
}