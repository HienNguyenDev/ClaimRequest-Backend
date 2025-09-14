using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;

using ClaimRequest.Domain.Claims;

namespace ClaimRequest.Application.Attendance.GetAbnormalCaseWithoutClaim
{
    public class GetAbnormalCaseWithoutClaimQueryHandler(IDbContext dbContext, IUserContext userContext) : IQueryHandler<GetAbnormalCaseWithoutClaimQuery, List<GetAbnormalCaseWithoutClaimQueryResponse>>
    {
        public async Task<Result<List<GetAbnormalCaseWithoutClaimQueryResponse>>> Handle(GetAbnormalCaseWithoutClaimQuery request, CancellationToken cancellationToken)
        {
            var userId = userContext.UserId;
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
            var list =  dbContext.AbnormalCases.Where(x=>x.UserId == userId 
                                                         && (!x.ClaimDetails.Any() || !x.ClaimDetails.Any(c => c.Claim.Status != ClaimStatus.Rejected && c.Claim.Status != ClaimStatus.Cancel)) 
                                                         && x.WorkDate >= startDate 
                                                         && x.WorkDate <= endDate);
            
            var totalcount = await list.CountAsync(cancellationToken);
              
            var result = await list.Select(x=> new GetAbnormalCaseWithoutClaimQueryResponse()
            {
                WorkDate = x.WorkDate,
            }).ToListAsync();
            
            return Result.Success(result);


        }
    }
}
