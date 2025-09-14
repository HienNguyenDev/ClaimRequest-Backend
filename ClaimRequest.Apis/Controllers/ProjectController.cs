using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.Projects.CreateProject;
using ClaimRequest.Application.Projects.GetCurrentUserIsPM;
using ClaimRequest.Application.Projects.GetListProjects;
using ClaimRequest.Application.Projects.GetMemberByRoleInProject;
using ClaimRequest.Application.Projects.GetProjectByName;
using ClaimRequest.Application.Projects.GetProjectDetails;
using ClaimRequest.Application.Projects.GetProjectMembers;
using ClaimRequest.Application.Projects.RemoveUserFromProject;
using ClaimRequest.Application.Projects.UpDateProject;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProjectController(ISender mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("project/get-list")]
        public async Task<IResult> GetListDepartment([FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            Result<Page<GetListProjectsResponse>> result = await _mediator.Send(new GetListProjectsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            }, cancellationToken);
            return result.MatchOk();
        }

        [Authorize]
        [HttpGet("project/get-members")]
        public async Task<IResult> GetListMembers(Guid projectId, CancellationToken cancellationToken)
        {
            GetProjectMembersQuery query = new GetProjectMembersQuery(projectId);
            Result<List<GetProjectMembersResponse>> result = await _mediator.Send(query, cancellationToken);
            return result.MatchOk();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost("project/create")]
        public async Task<IResult> CreateProject([FromBody] CreateProjectRequest request,
                                                            CancellationToken cancellationToken)
        {
            CreateProjectCommand command = new CreateProjectCommand
            {
                Name = request.Name,
                Code = request.Code,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Description = request.Description,
                DepartmentId = request.DepartmentId,
                CustomerName = request.CustomerName,
            };
            
            Result<CreateProjectCommandResponse> result = await _mediator.Send(command, cancellationToken);
            return result.MatchCreated(Id => $"/project/{Id}");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPut("project/update-project")]
        public async Task<IResult> UpdateProject([FromBody] UpdateProjectRequest request,
                                                        CancellationToken cancellationToken)
        {
            var command = new UpDateProjectCommand
            {
                ProjectId = request.ProjectId,
                Name = request.Name,
                Code = request.Code,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
            };
            Result result = await _mediator.Send(command, cancellationToken);

            return result.MatchOk();
        }
        
        [Authorize]
        [HttpGet("project/{projectId}")]
        public async Task<IResult> GetProjectDetails(Guid projectId, CancellationToken cancellationToken)
        {
            GetProjectDetailsQuery query = new GetProjectDetailsQuery(projectId);
            Result<GetProjectDetailsResponse> result = await _mediator.Send(query, cancellationToken);
            return result.MatchOk();
        }

        [Authorize]
        [HttpGet("project/search-by-name/{Name}")]
        public async Task<IResult> GetProjectByName(string Name, CancellationToken cancellationToken)
        {
            GetProjectByNameQuery query = new GetProjectByNameQuery(Name);
            Result<List<GetProjectByNameResponse>> result = await _mediator.Send(query, cancellationToken);
            return result.MatchOk();
        }
        
        [Authorize]
        [HttpGet("project/get-members-by-role/{projectID}/{memberRole}")]
        public async Task<IResult> GetMembersByRole(Guid projectID, string memberRole,
           CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetMemberByRoleInProjectQuery()
            {
                Id = projectID,
                Role = memberRole,
            }, cancellationToken);


            return result.MatchOk();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpDelete("project/remove-member")]
        public async Task<IResult> RemoveUserFromProject([FromBody] RemoveUserFromProjectCommand command, 
            CancellationToken cancellationToken)
        {
            Result result = await _mediator.Send(command, cancellationToken);
            return result.MatchOk();
        }
        
        [Authorize]
        [HttpGet("project/get-project-current-user-is-PM")]
        public async Task<IResult> GetProjectCurrentUserIsPM(CancellationToken cancellationToken)
        {
            GetProjectCurrentUserIsPMQuery query = new GetProjectCurrentUserIsPMQuery();
            Result<List<GetProjectCurrentUserIsPMQueryResponse>> result = await _mediator.Send(query, cancellationToken);
            return result.MatchOk();
        }
    }
}
