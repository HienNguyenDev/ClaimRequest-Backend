using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.EmailTemplates.GetEmailTemplate;

public class GetEmailTemplateQueryHandler(IDbContext context) : IQueryHandler<GetEmailTemplateQuery, List<GetEmailTemplateResponse>>
{
    public async Task<Result<List<GetEmailTemplateResponse>>> Handle(GetEmailTemplateQuery request, CancellationToken cancellationToken)
    {
        var list = await context.EmailTemplates.ToListAsync(); 
        var response =  list.Select(x => new GetEmailTemplateResponse() { 
                Id = x.Id,
                Content = x.Content,
                Header = x.Header, 
                MainContent = x.MainContent         
            })
            .ToList();
        await context.SaveChangesAsync(cancellationToken);
            
        return response;
    }
}