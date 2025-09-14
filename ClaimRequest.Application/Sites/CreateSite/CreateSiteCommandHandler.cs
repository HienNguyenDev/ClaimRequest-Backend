using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.SitesAndRooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Sites
{
    public class CreateSiteCommandHandler(IDbContext dbContext) : ICommandHandler<CreateSiteCommand, CreateSiteCommandResponse>
    {
        public async Task<Result<CreateSiteCommandResponse>> Handle(CreateSiteCommand request, CancellationToken cancellationToken)
        {
            var site = new Site { Id = Guid.NewGuid(), Name = request.Name, };
            dbContext.Sites.Add(site);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            var response = new CreateSiteCommandResponse
            {
               Id = site.Id,
               Name = site.Name,
            };

            return response;
        }
    }
}
