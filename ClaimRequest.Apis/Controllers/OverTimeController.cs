using ClaimRequest.Apis.Extensions;
using ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeEffort;
using ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeRequest;
using ClaimRequest.Application.ClaimOverTimes.ConfirmOverTimeEffort;
using ClaimRequest.Application.ClaimOverTimes.GetListOvertimeRequestCurrentUserCreated;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeGroupByUserEffortOfARequest;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToApprove;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToPaid;
using ClaimRequest.Application.ClaimOverTimes.GetListOverTimeRequestCurrentUserHasBeenPicked;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortToConfirm;
using ClaimRequest.Application.ClaimOverTimes.RejectOverTimeEffort;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeRequestToApprove;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CreateOverTimeRequest = ClaimRequest.Application.ClaimOverTimes.CreateOverTimeRequest.CreateOverTimeRequest;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeDateToDeclareEffort;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Application.ClaimOverTimes.RejectOverTimeRequest;
using ClaimRequest.Application.ClaimOverTimes.GetOverTimeEffortByOTMember;
using ClaimRequest.Application.ClaimOverTimes.CreateOverTimeEffort;
using ClaimRequest.Application.ClaimOverTimes.PaidOverTimeEffort;

namespace ClaimRequest.Apis.Controllers;


[Route("api/")]
[ApiController]

public class OverTimeController : ControllerBase
{
    private readonly ISender _mediator;

    public OverTimeController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = "ProjectManager")]
    [HttpPost("overtime/create-overtime-request")]
    public async Task<IResult> CreateOverTime([FromBody] Requests.CreateOverTimeRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateOverTimeRequest.Command
        {
            ProjectId = request.ProjectId,
            ApproverId = request.ApproverId,
            OverTimeDates = request.OverTimeDates,
            OverTimeMembersIds = request.OverTimeMembersIds,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            RoomId = request.RoomId,
            HasWeekday = request.HasWeekday,
            HasWeekend = request.HasWeekend,
        };
        
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }    
    
    [Authorize(Roles = "ProjectManager")]
    [HttpPut("overtime/confirm-overtime-effort/{effortId}")]
    public async Task<IResult> ConfirmOverTimeEntry(Guid effortId, CancellationToken cancellationToken)
    {
        ConfirmOverTimeEffortCommand command = new ConfirmOverTimeEffortCommand
        {
            Id = effortId,
        };
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }

    [Authorize(Roles = "BULeader")]
    [HttpPut("overtime/approve-overtime-entry/{effortId}")]
    public async Task<IResult> ApproveOverTimeEntry(Guid effortId, CancellationToken cancellationToken)
    {
        ApproveOverTimeEffortCommand command = new ApproveOverTimeEffortCommand
        {
            Id = effortId,
        };
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }

    [Authorize]
    [HttpGet("overtime/get-effort-group-by-user-of-request/{requestId}")]
    public async Task<IResult> GetOverTimeGroupByUserEffortOfARequest(Guid requestId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        GetOverTimeGroupByUserEffortOfARequestQuery query = new GetOverTimeGroupByUserEffortOfARequestQuery
        {
            Id = requestId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        Result<Page<GetOverTimeGroupByUserEffortOfARequestQueryResponse>> result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }

    [Authorize(Roles = "BULeader")]
    [HttpPut("overtime/approve-overtime-request/{requestId}")]
    public async Task<IResult> ApproveOverTimeRequest(Guid requestId, CancellationToken cancellationToken)
    {
        ApproveOverTimeRequestCommand command = new ApproveOverTimeRequestCommand
        {
            Id = requestId,
        };
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpGet("overtime/get-list-overtime-request-current-user-created/{status}")]
    public async Task<IResult> GetUserOverTimeRequest(OverTimeRequestStatus status, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        Result<Page<GetListOvertimeRequestCurrentUserCreatedQueryResponse>> result = await _mediator.Send(new GetListOvertimeRequestCurrentUserCreatedQuery()
        {
            Status = status,
            PageNumber = pageNumber,
            PageSize = pageSize
        }, cancellationToken);
        return result.MatchOk();
    }
    
    [Authorize(Roles = "BULeader, ProjectManager, Finance")]
    [HttpPut("overtime/reject-overtime-effort/{id}")]
    
    public async Task<IResult> RejectOverTimeEffort(Guid id, CancellationToken cancellationToken)
    {
        
        
        Result result = await _mediator.Send(new RejectOverTimeEffortCommand(){Id = id}, cancellationToken);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpGet("overtime/get-overtime-request-current-user-has-been-picked")]
    public async Task<IResult> GetOverTimeRequestByCurrentUserInOverTimeMember( [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        GetListOverTimeRequestCurrentUserHasBeenPickedQuery query = new GetListOverTimeRequestCurrentUserHasBeenPickedQuery
        {
        
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        Result<Page<GetListOverTimeRequestCurrentUserHasBeenPickedQueryResponse>> result = await _mediator.Send(query, cancellation);
        return result.MatchOk();

    }
    
    [Authorize(Roles = "BULeader")]
    [HttpGet("overtime/get-overtime-requests-to-approve")]
    public async Task<IResult> GetOverTimeRequests([FromQuery] int pageNumber, [FromQuery] int pageSize,
        CancellationToken cancellation)
    {
        Result<Page<GetOverTimeRequestToApproveResponse>> result =
            await _mediator.Send(new GetOverTimeRequestToApproveQuery { PageNumber = pageNumber, PageSize = pageSize },
                cancellation);
        return result.MatchOk();
    }
    
    [Authorize(Roles = "BULeader")]
    [HttpGet("overtime/get-overtime-effort-to-approve")]
    public async Task<IResult> GetOverTimeEffortToApprove([FromQuery] int pageNumber, [FromQuery] int pageSize,
        CancellationToken cancellation)
    {
        Result<Page<GetOverTimeEffortToApproveResponse>> result =
            await _mediator.Send(new GetOverTimeEffortToApproveQuery { PageNumber = pageNumber, PageSize = pageSize },
                cancellation);
        return result.MatchOk();
    }
    
    [Authorize(Roles = "Finance")]
    [HttpGet("overtime/get-overtime-effort-to-paid")]
    public async Task<IResult> GetOverTimeEffortToPaid([FromQuery] int pageNumber, [FromQuery] int pageSize,
        CancellationToken cancellation)
    {
        Result<Page<GetOverTimeEffortToPaidResponse>> result =
            await _mediator.Send(new GetOverTimeEffortToPaidQuery { PageNumber = pageNumber, PageSize = pageSize },
                cancellation);
        return result.MatchOk();
    }

    [Authorize(Roles = "BULeader")]
    [HttpPut("overtime/reject-overtime-request/{id}")]
    public async Task<IResult> RejectOverTimeRequest(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new RejectOverTimeRequestCommand { Id = id }, cancellationToken);
        return result.MatchOk();
    }

    [Authorize(Roles = "ProjectManager")]
    [HttpGet("overtime/get-overtime-effort-to-confirm")]
    public async Task<IResult> GetOverTimeEffortToConfirm([FromQuery] int pageNumber, [FromQuery] int pageSize,
        CancellationToken cancellation)
    {
        Result<Page<GetOverTimeEffortToConfirmResponse>> result =
            await _mediator.Send(new GetOverTimeEffortToComfirmQuery { PageNumber = pageNumber, PageSize = pageSize },
                cancellation);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpGet("overtime/get-date-to-declare-effort/{requestId}")]
    public async Task<IResult> GetOverTimeDateToDeclareEffort(Guid requestId, CancellationToken cancellationToken)
    {
        GetOverTimeDateToDeclareEffortQuery command = new GetOverTimeDateToDeclareEffortQuery
        {
            Id = requestId,
        };
        Result<List<GetOverTimeDateToDeclareEffortQueryResponse>> result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpPost("overtime/create-overtime-effort")]
    public async Task<IResult> CreateOverTimeEffort([FromBody] Requests.CreateOverTimeEffort request, CancellationToken cancellationToken)
    {
        CreateOverTimeEffortCommand command = new CreateOverTimeEffortCommand
        {
            RequestId = request.RequestId,
            OverTimeMemberId = request.OverTimeMemberId,
            OverTimeDateId = request.OverTimeDateId,
            DayHours = request.DayHours,
            NightHours = request.NightHours,
            TaskDescription = request.TaskDescription
        };
        Result result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }

    [Authorize]
    [HttpGet("overtime/get-effort")]
    public async Task<IResult> GetOverTimeEffortByOTMember([FromQuery] int pageNumber, [FromQuery] int pageSize, OverTimeEffortStatus effortStatus, CancellationToken cancellationToken)
    {
        Result<Page<GetOverTimeEffortByOTMemberResponse>> result = await _mediator.Send(new GetOverTimeEffortByOTMemberQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Status = effortStatus,
        }, cancellationToken);
        return result.MatchOk();
    }

    [Authorize(Roles = "Finance")]
    [HttpPut("overtime/paid-overtime-effort/{id}")]
    public async Task<IResult> PaidOverTimeEffort(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new PaidOverTimeEffort.PaidOverTimeEffortCommand() { Id = id }, cancellationToken);
        return result.MatchOk();
    }
}