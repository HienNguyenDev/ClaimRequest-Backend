using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonApplication.GetReasons;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.ReasonApplication.GetReasonsByReasonTypeId
{
    public class GetReasonsByReasonTypeIdQueryHandler(IDbContext context) : IQueryHandler<GetReasonsByReasonTypeIdQuery, List<ReasonsResponse>>
    {
        public async Task<Result<List<ReasonsResponse>>> Handle(GetReasonsByReasonTypeIdQuery request, CancellationToken cancellationToken)
        {
            var list = await context.Reasons.Where(d => d.RequestTypeId == request.Id && d.IsOther == false).ToListAsync();

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
