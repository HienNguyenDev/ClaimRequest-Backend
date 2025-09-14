using ClaimRequest.Apis.Extensions;
using ClaimRequest.Application.Attendance.CheckIn;
using ClaimRequest.Application.Attendance.CheckOut;
using ClaimRequest.Application.Attendance.GetAbnormalCase;
using ClaimRequest.Application.Attendance.GetAbnormalCaseWithoutClaim;
using ClaimRequest.Application.Attendance.GetLateEarlyCase;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers;


[Route("api/")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly ISender _mediator;

    public AttendanceController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpGet("attendance/get-late-early-case")]
    public async Task<IResult> GetLateEarlyCase([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        GetLateEarlyQuery query = new GetLateEarlyQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query, cancellationToken);
        
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpPut("attendance/check-in")]
    public async Task<IResult> CheckIn(CancellationToken cancellation)
    {
        var command = new CheckInCommand();

        var result = await _mediator.Send(command,cancellation);
        return result.MatchOk();
    }

    [Authorize]
    [HttpPut("attendance/check-out")]
    public async Task<IResult> CheckOut(CancellationToken cancellation)
    {
        var command = new CheckOutCommand();

        var result = await _mediator.Send(command, cancellation);
        return result.MatchOk();
    }
    
    [Authorize]
    [HttpGet("attendance/get-abnormal-case")]
    public async Task<IResult> GetAbnormalCase([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        GetAbnormalQuery query = new GetAbnormalQuery
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        var result = await _mediator.Send(query, cancellationToken);
        
        return result.MatchOk();
    }

    [Authorize]
    [HttpGet("attendance/get-abnormal-case-without-claim")]
    public async Task<IResult> GetAbnormalCaseWithoutClaim(CancellationToken cancellationToken)
    {

        var result = await _mediator.Send(new GetAbnormalCaseWithoutClaimQuery(), cancellationToken);

        return result.MatchOk();
    }
}