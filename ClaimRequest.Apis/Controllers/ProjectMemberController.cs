using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Infrastructure;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Projects.CreateProjectMember;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProjectMemberController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProjectMemberController(ISender mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("members/add")]
        public async Task<IResult> AddProjectMember([FromBody] CreateProjectmemberRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProjectmemberCommand
            {
                ProjectID = request.ProjectID,
                UserID = request.UserID,
                RoleInProject = request.RoleInProject
            };

            Result<ProjectMember> result = await _mediator.Send(command, cancellationToken);
            return result.Match(Results.Created, CustomResults.Problem);
        }
    }
}
