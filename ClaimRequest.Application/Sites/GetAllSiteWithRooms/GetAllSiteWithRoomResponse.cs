namespace ClaimRequest.Application.Sites.GetAllSiteWithRooms;

public class GetAllSiteWithRoomResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<GetRooms> Rooms { get; set; }
}

public class GetRooms
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}