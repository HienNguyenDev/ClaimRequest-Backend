using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Users.CreateDepartment;
using ClaimRequest.Application.Users.GetDepartments;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers;

[Route("api/")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly ISender _mediator;

    public DepartmentController(ISender mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("department/create")]
    public async Task<IResult> CreateDepartment([FromBody] CreateDepartmentRequest request, CancellationToken cancellationToken)
    {
        CreateDepartmentCommand command = new CreateDepartmentCommand
        {
            Name = request.Name,
            Code = request.Code,
            Description = request.Description
        };
        Result<CreateDepartmentCommandResponse> result = await _mediator.Send(command, cancellationToken);
        Console.Write(result);
        return result.MatchCreated(id => $"/department/{id}");
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("department/get")]
    public async Task<IResult> GetDepartments(CancellationToken cancellation)
    {
        Result<List<GetDepartmentsResponse>> result = await _mediator.Send(new GetDepartmentsQuery(), cancellation);
        return result.MatchOk();
    }
}