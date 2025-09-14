using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Sites.GetAllSiteWithRooms;

public class GetAllSiteWithRoomQueryHandler(IDbContext context)
    : IQueryHandler<GetAllSiteWithRoomQuery, List<GetAllSiteWithRoomResponse>>
{
    public async Task<Result<List<GetAllSiteWithRoomResponse>>> Handle(GetAllSiteWithRoomQuery request, CancellationToken cancellationToken)
    {
        var sites = await context.Sites
            .Select(r => new GetAllSiteWithRoomResponse
            {
                Id = r.Id,
                Name = r.Name,
                Rooms = r.Rooms.Select(u=> new GetRooms() 
                { 
                    Id = u.Id,
                    Name = u.Name,
                }).ToList(),
            }).ToListAsync();
        return Result<GetAllSiteWithRoomResponse>.Success(sites);
    }
}