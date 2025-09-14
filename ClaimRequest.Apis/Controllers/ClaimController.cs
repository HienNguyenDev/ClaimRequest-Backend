using ClaimRequest.Apis.Extensions;
using ClaimRequest.Application.Claims.ConfirmClaim;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Claims.ApproveClaim;
using ClaimRequest.Application.Claims.CreateClaim;
using ClaimRequest.Application.Claims.CancelClaim;
using ClaimRequest.Application.Claims.SearchClaimByEmail;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ClaimRequest.Application.Claims.GetPersonalByStatusClaim;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Application.Claims.PaidClaim;
using ClaimRequest.Application.Claims.DownloadPersonalClaim;
using ClaimRequest.Application.Claims.GetApprovedClaimToPaid;
using ClaimRequest.Application.Claims.GetClaimToApprove;
using ClaimRequest.Application.Claims.GetClaimToConfirm;
using Microsoft.AspNetCore.Authorization;
using ClaimRequest.Application.Claims.RejectClaim;

namespace ClaimRequest.Apis.Controllers;

[Route("api/")]
[ApiController]
public class ClaimController : ControllerBase
{
    private readonly ISender _mediator;

    public ClaimController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    
    [Authorize]
    [HttpPost("claim/create")]
    public async Task<IResult> CreateClaim([FromBody] CreateClaimRequest request, CancellationToken cancellationToken)
    {
        CreateClaimCommand command = new CreateClaimCommand
        {

            ReasonId  = request.ReasonId,
            OtherReasonText = request.OtherReasonText,
            StartDate  = request.StartDate,
            EndDate = request.EndDate,
            DatesForClaim = request.DatesForClaim,
            SupervisorId  = request.SupervisorId,
            ApproverId  = request.ApproverId,
            Partial = request.Partial,
            ExpectApproveDay = request.ExpectedApproveDay,
            InformTos = request.InformTos,
            ClaimFee = request.ClaimFee,
        };
        
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    
    [Authorize(Roles = "BULeader")]
    [HttpPut("claim/approve/{id}")]
    public async Task<IResult> ApproveClaims(Guid id, CancellationToken cancellation)
    {
        var result = await _mediator.Send( new ApproveClaimCommand() { Id = id  },cancellation);
            

        return result.MatchOk();
    }
    
    
    [Authorize(Roles = "BULeader")]
    [HttpGet("claim/get-claim-to-approve")]
    public async Task<IResult> GetApproveClaims([FromQuery]int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetCLaimToApproveResponse>> result = await _mediator.Send(new GetClaimToApproveQuery{PageNumber = pageNumber,
            PageSize = pageSize}, cancellation);


            return result.MatchOk();
    }
    
    
    
    [Authorize(Roles = "Finance")]
    [HttpPut("claim/paid/{claimId}")]
    public async Task<IResult> PaidClaim(Guid claimId, CancellationToken cancellationToken)
    {
        var command = new PaidClaimCommand(claimId);
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
        
    [Authorize(Roles = "ProjectManager, BULeader")]
    [HttpPut("claim/confirm/{claimId}")]
    public async Task<IResult> ConfirmClaim(Guid claimId, CancellationToken cancellationToken)
    {
        ConfirmClaimCommand command = new ConfirmClaimCommand 
        {
            Id = claimId,
        };
        Result<Guid> result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    
    [Authorize(Roles = "Finance")]
    [HttpGet("claim/download-personal-claims")]
    public async Task<IActionResult> DownloadPersonalClaims(CancellationToken cancellationToken)
    {
        var fileBytes = await _mediator.Send(new DownloadPersonalClaimQuery(), cancellationToken);
        return File(fileBytes.Value, 
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "PersonalClaims.xlsx");
    }

    [Authorize]
    [HttpGet("claim/get-personal-by-status/{status}")]
    public async Task<IResult> GetClaimPersonalByStatusAsync(ClaimStatus status, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        GetPersonalByStatusClaimQuery query = new GetPersonalByStatusClaimQuery
        {
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        Result<Page<GetPersonalByStatusClaimQueryResponse>> result = await _mediator.Send(query, cancellation);
        return result.MatchOk();

    }

    [Authorize(Roles = "ProjectManager, BULeader")]
    [HttpGet("claim/get-claim-to-confirm")]

    public async Task<IResult> GetConfirmedClaims([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetClaimToConfirmQueryResponse>> result = await _mediator.Send(new GetClaimToConfirmQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        }, cancellation);


        return result.MatchOk();
    }
    
    [Authorize(Roles = "Finance")]
    [HttpGet("claim/get-approved-claim-to-paid")]
    public async Task<IResult> GetApprovedClaims([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetApprovedClaimToPaidResponse>> result = await _mediator.Send(new GetApprovedClaimToPaidQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        }, cancellation);
        
        return result.MatchOk();
    }

    [Authorize]
    [HttpPut("claim/cancel/{Claimid}")]
    public async Task<IResult> CancelClaim(Guid Claimid, CancellationToken cancellationToken)
    {
            var command = new CancelClaimCommand(Claimid);
            Result result = await _mediator.Send(command, cancellationToken);
            return result.MatchOk();
    }

    [Authorize(Roles = "BULeader, ProjectManager")]
    [HttpPut("claim/Reject/{Claimid}")]
    public async Task<IResult> RejectClaim(Guid Claimid, CancellationToken cancellationToken)
    {
        var command = new RejectClaimCommand()
        {
            Id = Claimid,
        };
        Result<string> result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }

    [Authorize]
    [HttpGet("claim/search-claim-by-email")]
    public async Task<IResult> SearchClaimByEmail([FromQuery] string email,[FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        Result<Page<SearchClaimByEmailResponse>> result = await _mediator.Send(new SearchClaimByEmailQuery
        {
            Email = email,
            PageNumber = pageNumber,
            PageSize = pageSize
        }, cancellationToken);

        return result.MatchOk();
    }

}