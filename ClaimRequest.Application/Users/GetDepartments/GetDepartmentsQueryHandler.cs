using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Users.GetDepartments;

public class GetDepartmentsQueryHandler(IDbContext context) : IQueryHandler<GetDepartmentsQuery, List<GetDepartmentsResponse>>
{
    public async Task<Result<List<GetDepartmentsResponse>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
    {
        var list = await context.Departments.ToListAsync(); 
        var response =  list.Select(x => new GetDepartmentsResponse() { 
                Id = x.Id,
                Name = x.Name,
                Code = x.Code, 
                Description = x.Description         
            })
            .ToList();
        await context.SaveChangesAsync(cancellationToken);
            
        return response;
    }
}