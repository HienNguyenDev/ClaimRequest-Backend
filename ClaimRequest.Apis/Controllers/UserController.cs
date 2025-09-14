using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Users.ActivateUser;
using ClaimRequest.Application.Users.ChangePassword;
using ClaimRequest.Application.Users.CreateUser;
using ClaimRequest.Application.Users.GetCurrentUser;
using ClaimRequest.Application.Users.GetUserBuLeader;
using ClaimRequest.Application.Users.GetUserProjectManager;
using ClaimRequest.Application.Users.GetUsers;
using ClaimRequest.Application.Users.SearchUserByEmail;
using ClaimRequest.Application.Users.UpdateUsers;
using ClaimRequest.Application.Users.InActiveUser;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClaimRequest.Application.Users.ReActiveUser;
using ClaimRequest.Application.Users.ExportPersonalSalary;

namespace ClaimRequest.Apis.Controllers;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("user/create")]
    public async Task<IResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        CreateUserCommand command = new CreateUserCommand
        {
            Email = request.Email,
            FullName = request.FullName,
            Rank = request.Rank,
            Role = request.Role,
            BaseSalary = request.BaseSalary,
            DepartmentId = request.Department,
        };
        
        Result<CreateUserCommandResponse> result = await _mediator.Send(command, cancellationToken);
        return result.MatchCreated(id => $"/user/{id}");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("user/get-users")]
    public async Task<IResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellation)
    {
        Result<Page<GetUsersResponse>> result = await _mediator.Send(new GetUsersQuery
                                                                         {
                                                                             PageNumber = pageNumber,
                                                                             PageSize = pageSize,
                                                                         }, cancellation);
        return result.MatchOk();
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("user/update-users")]
    public async Task<IResult> UpdateUser([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        
        var command = new UpdateUserCommand
        {
            UserId = request.UserId,
            FullName = request.FullName,
            Email = request.Email,
            Role = request.Role,
            Rank = request.Rank,
            BaseSalary = request.BaseSalary,
            Status = request.Status,
            DepartmentId = request.DepartmentId
        };

        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchOk();
    }
    
    
    [Authorize]
    [HttpGet("user/get-user-buleader")]
    public async Task<IResult> GetUserBuLeader(CancellationToken cancellation)
    {
        var result = await _mediator.Send(new GetUserBuLeaderQuery());
        return result.MatchOk();
    }
    
    
    [Authorize]
    [HttpGet("user/get-user-supervisor")]
    public async Task<IResult> GetUserSupervisor(CancellationToken cancellation)
    {
        var result = await _mediator.Send(new GetUserSupervisor.Query());
        return result.MatchOk();
    }

    [Authorize]
    [HttpGet("user/search-user-by-email")]
    public async Task<IResult> GetUserByEmail(string Email, CancellationToken cancellation)
    {
        SearchUserByEmailQuery query = new SearchUserByEmailQuery(Email);
        Result<SearchUserByEmailItem> result = await _mediator.Send(query, cancellation);
        return result.MatchOk();
    }

    [Authorize]
    [HttpPut("user/change-password")]
    public async Task<IResult> ChangePassword([FromBody] ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangePasswordCommand
        {
            CurrentPassword = request.CurrentPassword,
            NewPassword = request.NewPassword
        };

        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchOk();
    }
    
    [HttpPut("user/activate-user")]
    public async Task<IResult> ActivateUser([FromBody] ActivateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ActivateUserCommand()
        {
            Email = request.Email,
            NewPassword = request.NewPassword
        };

        var result = await _mediator.Send(command, cancellationToken);

        return result.MatchOk();
    }
    
    [Authorize]
    [HttpGet("user/get-current-user")]
    public async Task<IResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        GetCurrentUserQuery query = new GetCurrentUserQuery();
        Result<GetCurrentUserResponse> result = await _mediator.Send(query, cancellationToken);
        return result.MatchOk();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("user/check-inactive")]
    public async Task<IResult> CheckInactiveUser([FromQuery] InActiveUserRequest request, CancellationToken cancellationToken)
    {
        var command = new InActiveUserCommand()
        {
            Email = request.Email
        };

        Result result = await _mediator.Send(command, cancellationToken);

        return result.MatchOk();
    }

    [Authorize]
    [HttpPut("user/check-reactive")]
    public async Task<IResult> ReactivateUser([FromQuery] ReActiveUserRequest request, CancellationToken cancellationToken)
    {
        var command = new ReActiveUserCommand
        {
            Email = request.Email
        };

        Result result = await _mediator.Send(command, cancellationToken);

        return result.MatchOk();
    }

    [Authorize(Roles = "Finance")]
    [HttpGet("user/export-list-salary")]
    public async Task<IActionResult> ExportListSalary(CancellationToken cancellationToken)
    {
        var fileBytes = await _mediator.Send(new ExportListSalaryQuery(), cancellationToken);
        return File(fileBytes.Value,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "SalaryMonthReport.xlsx");
    }
}