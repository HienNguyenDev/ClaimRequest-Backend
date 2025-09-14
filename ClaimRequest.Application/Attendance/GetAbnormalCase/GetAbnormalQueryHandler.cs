using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Attendance.GetAbnormalCase;

public class GetAbnormalQueryHandler(IDbContext dbContext, IUserContext userContext) : IQueryHandler<GetAbnormalQuery, Page<GetAbnormalItem>>
{
    public async Task<Result<Page<GetAbnormalItem>>> Handle(GetAbnormalQuery query, CancellationToken cancellationToken)
    {
        var userid = userContext.UserId;
        var today = DateTime.UtcNow.Date; 
    
        
        DateOnly startDate;
        DateOnly endDate;
    
        if (today.Day > 3)
        {
            startDate = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);  
            endDate = new DateOnly(today.Year, today.Month, today.Day);  
        }
        else
        {
            startDate = new DateOnly(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1).AddMonths(-1); 
            endDate = new DateOnly(today.Year, today.Month, today.Day);  
        }
        var list = dbContext.AbnormalCases
                                                        .Where(c => c.UserId == userid 
                                                                                && c.WorkDate >= startDate 
                                                                                && c.WorkDate <= endDate
                                                                                && (c.ClaimDetails.All(cd => cd.Claim.Status != ClaimStatus.Approved) || !c.ClaimDetails.Any()));
        var totalCount = await list.CountAsync(cancellationToken);
        
        var result = await list
            .ApplyPagination(query.PageNumber, query.PageSize)
            .Select(record => new GetAbnormalItem
                {
                    WorkDate = record.WorkDate,
                    RecordType = record.AbnormalType,
                    Status = record.ClaimDetails.OrderByDescending(x => x.Claim.CreatedAt).FirstOrDefault() != null ? record.ClaimDetails.OrderByDescending(x => x.Claim.CreatedAt).FirstOrDefault()!.Claim.Status : null,
                    PartialDay = record.ClaimDetails.OrderByDescending(x => x.Claim.CreatedAt).FirstOrDefault() != null ? record.ClaimDetails.OrderByDescending(x => x.Claim.CreatedAt).FirstOrDefault()!.Claim.Partial : null,
                    ReasonType = record.ClaimDetails.OrderByDescending(x => x.Claim.CreatedAt).FirstOrDefault() != null ? record.ClaimDetails.OrderByDescending(x => x.Claim.CreatedAt).FirstOrDefault()!.Claim.Reason.ReasonType.Name : null,
                }).ToListAsync(cancellationToken);

        return new Page<GetAbnormalItem>(result, totalCount, query.PageNumber, query.PageSize);
    }
}