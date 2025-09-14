using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.ReasonApplication.GetReasons
{
    public class GetReasonsQueryHandler(IDbContext context) : IQueryHandler<GetReasonsQuery, List<ReasonsResponse>>
    {
        public  async Task<Result<List<ReasonsResponse>>> Handle(GetReasonsQuery request, CancellationToken cancellationToken)
        {
            var list = await context.Reasons.ToListAsync();

            var response = list.Select(x => new ReasonsResponse
            {
                Id = x.Id,
                RequestTypeId = x.RequestTypeId,
                Name = x.Name,
                IsOther = x.IsOther,
              
            }).ToList();

            await context.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}
