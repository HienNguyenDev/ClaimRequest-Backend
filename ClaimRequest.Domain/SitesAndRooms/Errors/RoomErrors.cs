using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.SitesAndRooms.Errors;

public static class RoomErrors
{
    public static Error NotFound(Guid? roomId) => Error.NotFound(
        "Rooms.NotFound",
        $"The room with the Id = '{roomId}' was not found");
}

