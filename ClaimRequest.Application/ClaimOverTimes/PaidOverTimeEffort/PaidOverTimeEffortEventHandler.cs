using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.ClaimOverTime.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.ApproveOverTimeEffort;

public class PaidOverTimeEffortEventHandler(IDbContext dbContext) : INotificationHandler<OverTimeEffortPaidEvent>
{
    public async Task Handle(OverTimeEffortPaidEvent notication, CancellationToken cancellationToken)
    {
        var thisMonthYear = new  DateOnly(DateTime.Now.Year, DateTime.Now.Month,1);

        var salaryPerMonth = await  dbContext.Salaries.SingleAsync(s => s.UserId ==notication.UserId 
                                                                 && s.MonthYear == thisMonthYear, cancellationToken);
        
        salaryPerMonth.OvertimeHours += notication.Effort;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}