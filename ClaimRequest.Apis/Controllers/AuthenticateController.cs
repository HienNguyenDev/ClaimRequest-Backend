using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Users.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers;


[Route("api/")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly ISender _mediator;

    public AuthenticateController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost("auth/login")]
    public async Task<IResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        LoginUserCommand command = new LoginUserCommand
        {
            Email = request.Email,
            Password = request.Password
        };
        
        var  result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
    
    [HttpPost("auth/loginWithRefreshToken")]
    public async Task<IResult> LoginWithRefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        LoginUserWithRefreshToken.LoginByRefreshTokenCommand command = new LoginUserWithRefreshToken.LoginByRefreshTokenCommand
        {
            RefreshToken = refreshToken
        };
        
        var  result = await _mediator.Send(command, cancellationToken);
        return result.MatchOk();
    }
    
}