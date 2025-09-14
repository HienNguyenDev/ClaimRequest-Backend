using ClaimRequest.Domain.Users;
using ClaimRequest.Infrastructure.Database;
using Quartz;

namespace ClaimRequest.Infrastructure;

public class CreateSalaryRecordJob(ApplicationDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var thisMonthYear = new  DateOnly(DateTime.Now.Year, DateTime.Now.Month,1);
        // create salary record every 3rd of month. 
        var salaries = dbContext.Users.Select(u => new SalaryPerMonth
        {
            Id = Guid.NewGuid(),
            UserId = u.Id,
            BaseSalary = 0,
            AbnormalCases = 0,
            LateEarlyLeaveCases = 0,
            OvertimeHours = 0,
            MonthYear = thisMonthYear,
            TotalSalary = 0,
            FinePerAbnormalCase = 0,
            FinePerLateEarlyCase = 0,
            SalaryPerOvertimeHour = 0,
            OtherMoney = 0
        });
        dbContext.Salaries.AddRange(salaries);
        await dbContext.SaveChangesAsync();
    }
}