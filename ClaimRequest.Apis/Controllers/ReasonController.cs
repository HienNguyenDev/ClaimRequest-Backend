using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.ReasonApplication.CreateReason;
using ClaimRequest.Application.ReasonApplication.GetReasons;
using ClaimRequest.Application.ReasonApplication.GetReasonsByReasonTypeId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReasonController : ControllerBase
    {
        private readonly ISender _sender;

        public ReasonController(ISender sender) => _sender = sender;

        [Authorize(Roles = "Admin")]
        [HttpPost("reason/create")]
        public async Task<IResult> CreateReasonAsync(
                            [FromBody] CreateReasonRequest request,
                            CancellationToken cancellationToken)
        {
            var command = new CreateReasonCommand {
                RequestTypeId = request.RequestTypeId,
                Name = request.Name ,
            };

            var result = await _sender.Send(command, cancellationToken);
         


            return result.MatchCreated(id => $"//{id}");


        }
        
        [Authorize]
        [HttpGet("reason/get-reasons")]
        public async Task<IResult> GetReasonsAsync(CancellationToken cancellation)
        {
            var result = await _sender.Send(new GetReasonsQuery(), cancellation);


            return result.MatchOk();

        }

        [Authorize]
        [HttpGet("reason/get-reasons-by-type-id/{id}")]
        public async Task<IResult> GetReasonsBuTypeIdAsync(CancellationToken cancellation, Guid id)
        {
            
            var result = await _sender.Send(new GetReasonsByReasonTypeIdQuery() { Id = id}, cancellation);


            return result.MatchOk();

        }
    }
}
