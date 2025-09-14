using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonTypeApplication.CreateReasonType;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Reasons;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.ReasonTypeApplication.GetReasonTypeList
{
    public class GetReasonTypeListQueryHandler(IDbContext context)
    : IQueryHandler<GetReasonTypeListQuery, List<CreateReasonTypeCommandResponse>>
    {
        public async Task<Result<List<CreateReasonTypeCommandResponse>>> Handle(GetReasonTypeListQuery request, CancellationToken cancellationToken)
        {
            var list = await context.ReasonTypes.ToListAsync(cancellationToken) ?? new List<ReasonType>();

            var response = list.Select(x => new CreateReasonTypeCommandResponse
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            await context.SaveChangesAsync(cancellationToken);
            return response;
        }
    }

}
