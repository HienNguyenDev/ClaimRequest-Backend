using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
namespace ClaimRequest.Application.Attendance.GetLateEarlyCase;


public class GetLateEarlyQueryHandler
        (IDbContext dbContext, 
        IUserContext userContext)
        : IQueryHandler<GetLateEarlyQuery, Page<LateEarlyQueryItem>>
{
    public async Task<Result<Page<LateEarlyQueryItem>>> Handle(GetLateEarlyQuery query, CancellationToken cancellationToken)
    {
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
        
        var userId = userContext.UserId;

        
        
        var list = dbContext.LateEarlyCases.Where(l => l.UserId == userId 
                                                       && !l.ClaimDetails.Any(x => x.Claim.Status != ClaimStatus.Cancel && x.Claim.Status != ClaimStatus.Rejected) 
                                                       && l.WorkDate >= startDate 
                                                       && l.WorkDate <= endDate);
        
        var totalCount = await list.CountAsync(cancellationToken);

        var result = await list.ApplyPagination(query.PageNumber, query.PageSize).Select(record =>
            new LateEarlyQueryItem
            {
                CheckInTime = record.CheckInTime,
                CheckoutTime = record.CheckoutTime,
                IsLateCome = record.IsLateCome, 
                IsEarlyLeave = record.IsEarlyLeave,
                LateTimeSpan = record.LateTimeSpan,
                EarlyTimeSpan = record.EarlyTimeSpan,
            }).ToListAsync(cancellationToken);

        return new Page<LateEarlyQueryItem>(result, totalCount, query.PageNumber, query.PageSize);
    }

}