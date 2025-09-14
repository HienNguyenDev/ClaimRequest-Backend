using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.Claims.Events;
using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.Claims.ApproveClaim;

public class ClaimHasFeePaidEventHandler(IDbContext dbContext) : INotificationHandler<ClaimHasFeePaidEvent>
{
    public async Task Handle(ClaimHasFeePaidEvent notification, CancellationToken cancellationToken)
    {
       var claim =  await dbContext.Claims.FindAsync(notification.ClaimId,  cancellationToken);
       var thisMonthYear = new  DateOnly(DateTime.Now.Year, DateTime.Now.Month,1);

       if (claim == null || claim.ClaimFee == null)
       {
           return;
       }
       
       var salaryPerMonth = await  dbContext.Salaries.SingleOrDefaultAsync(s => s.UserId == claim!.UserId 
                                                                       && s.MonthYear == thisMonthYear, cancellationToken);

       if (salaryPerMonth == null)
       {
           return;
       }
       
       salaryPerMonth.OtherMoney +=  claim!.ClaimFee!.Value;
       
       dbContext.Salaries.Update(salaryPerMonth);
       await dbContext.SaveChangesAsync();
    }
}