using ClaimRequest.Apis.Extensions;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClaimRequest.Application.SitesAndRooms.GetRoomBySite;

namespace ClaimRequest.Apis.Controllers;

[Route("api/")]
[ApiController]
public class SitesAndRoomsController : ControllerBase
{
    private readonly ISender _mediator;

    public SitesAndRoomsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("SitesAndRooms/get-room-by-site")]
    public async Task<IResult> GetRoomBySite([FromQuery] Guid SiteId, CancellationToken cancellationToken)
    {
        GetRoomBySiteQuery query = new GetRoomBySiteQuery(SiteId);
        Result<List<GetRoomBySiteItem>>
            
            result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }
}
