using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.SitesAndRooms.Errors;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.SitesAndRooms.GetRoomBySite
{
    public class GetRoomBySiteQueryHandler(IDbContext dbContext) : IQueryHandler<GetRoomBySiteQuery, List<GetRoomBySiteItem>>
    {
        public async Task<Result<List<GetRoomBySiteItem>>> Handle(GetRoomBySiteQuery request, CancellationToken cancellationToken)
        {
            var room = await dbContext.Rooms
                .Where(r => r.SiteId == request.SiteId)
                .Select(r => new GetRoomBySiteItem
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();
            if (!room.Any())
            {
                return Result.Failure<List<GetRoomBySiteItem>>(RoomErrors.NotFound(request.SiteId));
            }
            return Result<List<GetRoomBySiteItem>>.Success(room);
        }
     
    }
}
