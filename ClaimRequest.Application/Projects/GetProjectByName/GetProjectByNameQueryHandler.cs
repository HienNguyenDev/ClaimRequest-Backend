using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Projects.GetProjectByName;

public class GetProjectByNameQueryHandler(IDbContext context)
    : IQueryHandler<GetProjectByNameQuery, List<GetProjectByNameResponse>>
{
    public async Task<Result<List<GetProjectByNameResponse>>> Handle(GetProjectByNameQuery request,
        CancellationToken cancellationToken)
    {
        var project =
            context.Projects.Where(p => p.IsSoftDelete == 0 && request.Name == "" || p.Name.ToLower().Contains(request.Name.ToLower()));
            

        // if (!string.IsNullOrEmpty(request.Name))
        // {
        //     string searchPattern = $"%{request.Name.ToLower()}%";
        //     project = project.Where(p => EF.Functions.Like(p.Name.ToLower(), searchPattern));
        // }
        
        var result = await project
            .Select(x => new GetProjectByNameResponse
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Status = x.Status,
            })
            .ToListAsync(cancellationToken);

        return Result<List<GetProjectByNameResponse>>.Success(result);

    }
}