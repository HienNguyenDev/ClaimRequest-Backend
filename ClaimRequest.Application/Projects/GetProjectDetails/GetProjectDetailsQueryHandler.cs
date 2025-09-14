using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects.Errors;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.Projects.GetProjectDetails
{
    public class GetProjectDetailsQueryHandler(IDbContext context) : IQueryHandler<GetProjectDetailsQuery, GetProjectDetailsResponse>
    {
        public async Task<Result<GetProjectDetailsResponse>> Handle(GetProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.Where(p => p.IsSoftDelete == 0).Select(p => new GetProjectDetailsResponse
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
            }).FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (project == null)
            {
                return Result.Failure<GetProjectDetailsResponse>(ProjectErrors.NotFound(request.Id)
                );
            }

            return project;
        }
    }
}
