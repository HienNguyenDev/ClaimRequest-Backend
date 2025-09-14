using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.SitesAndRooms.CreateRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly ISender _sender;

        public RoomController(ISender sender) => _sender = sender;

        [Authorize(Roles = "Admin")]
        [HttpPost("room/create")]
        public async Task<IResult> CreateRoomAsync(
                            [FromBody] CreateRoomRequest request,
                            CancellationToken cancellationToken)
        {
            var command = new CreateRoomCommand
            {
                SiteId = request.SiteId,
                Name = request.Name,
            };

            var result = await _sender.Send(command, cancellationToken);
            return result.MatchCreated(id => $"//{id}");


        }
    }
}
