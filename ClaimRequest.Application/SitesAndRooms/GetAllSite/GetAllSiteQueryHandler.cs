using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonTypeApplication.CreateReasonType;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Reasons;
using ClaimRequest.Domain.SitesAndRooms;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.SitesAndRooms.GetAllSite
{
    public class GetAllSiteQueryHandler(IDbContext context)
    : IQueryHandler<GetAllSiteQuery, List<GetAllSiteResponse>>
    {
        public async Task<Result<List<GetAllSiteResponse>>> Handle(GetAllSiteQuery request, CancellationToken cancellationToken)
        {
            var list = await context.Sites.ToListAsync(cancellationToken) ?? new List<Site>();

            var response = list.Select(x => new GetAllSiteResponse
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            await context.SaveChangesAsync(cancellationToken);
            return response;
        }
    }
}
