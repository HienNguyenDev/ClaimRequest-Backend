using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Sites;
using ClaimRequest.Application.Sites.GetAllSiteWithRooms;
using ClaimRequest.Application.SitesAndRooms.GetAllSite;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class SiteController : Controller
    {
        private readonly ISender _sender;

        public SiteController(ISender sender) => _sender = sender;


        [Authorize]
        [HttpGet("site/get-all-sites")]
        public async Task<IResult> GetAllSiteAsync(CancellationToken cancellation)
        {
            Result<List<GetAllSiteResponse>> result = await _sender.Send(new GetAllSiteQuery(), cancellation);

            return result.MatchOk();

        }
        
        [Authorize]
        [HttpGet("site/get-all-sites-with-rooms")]
        public async Task<IResult> GetAllSiteWithRoomsAsync(CancellationToken cancellation)
        {
            Result<List<GetAllSiteWithRoomResponse>> result = await _sender.Send(new GetAllSiteWithRoomQuery(), cancellation);

            return result.MatchOk();

        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("site/create-site")]
        public async Task<IResult> CreateSite([FromBody] CreateSiteRequest request, CancellationToken cancellationToken)
        {
            CreateSiteCommand command = new CreateSiteCommand { Name = request.Name };

            var result = await _sender.Send(command, cancellationToken);

            return result.MatchCreated(id => $"/site/{id}");
             
        }
        
    }
}