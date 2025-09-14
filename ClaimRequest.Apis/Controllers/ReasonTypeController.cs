using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.ReasonTypeApplication.CreateReasonType;
using ClaimRequest.Application.ReasonTypeApplication.GetReasonTypeList;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ReasonTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public ReasonTypeController(ISender sender) => _sender = sender;

        
        [Authorize(Roles = "Admin")]
        [HttpPost("reason-type/create")]
        public async Task<IResult> CreateReasonTypeAsync(
                                    [FromBody] CreateReasonTypeRequest request,
                                    CancellationToken cancellationToken)
        {
            var command = new CreateReasonTypeCommand { Name = request.Name };

            var result = await _sender.Send(command, cancellationToken);

           
           
            return result.MatchCreated(id => $"//{id}");


        }

        [Authorize]
        [HttpGet("reason-type/get-reason-types")]
        public async Task<IResult> GetReasonTypesAsync(CancellationToken cancellation)
        {
            Result<List<CreateReasonTypeCommandResponse>> result = await _sender.Send(new GetReasonTypeListQuery(), cancellation);



            return result.MatchOk();

        }
    }
}
