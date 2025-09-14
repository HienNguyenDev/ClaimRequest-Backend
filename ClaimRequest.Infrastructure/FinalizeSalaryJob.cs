using ClaimRequest.Domain.Claims;
using ClaimRequest.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ClaimRequest.Infrastructure;

[DisallowConcurrentExecution]
public class FinalizeSalaryJob(ApplicationDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // ngay 3 moi thang tao ra record table salary per month cho tung user 
        
        //Job nay can finalize luong cua thang truoc.
        
        // Lay het tat ca user, voi salary record cua thang truoc ra.
        
        // voi tung user, get all abnormal case chua duoc tao don ra.
        
        // get all late early chua duoc tao don ra
        
        // get so gio overtime cua thang truoc ra
        
       
        var previousMonthYear = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);

        var salaryPerMonth = await  dbContext.Salaries.Where(s  => s.MonthYear == previousMonthYear).ToListAsync();

        var usersWithDataNeeded = await dbContext.Users.Select(user => new
        {
            Id = user.Id,
            NumberOfAbnormalCase = user.AbnormalCases.Count(c => c.ClaimDetails.All(cd => cd.Claim.Status != ClaimStatus.Approved)),
            NumberOfLateEarlyCase = user.LateEarlyCases.Count(c => c.ClaimDetails.All(x => x.Claim.Status != ClaimStatus.Approved)),
            BaseSalary = user.BaseSalary,
        }).ToDictionaryAsync(user => user.Id , user => user);

        
        var companyConfig = await dbContext.CompanySettings.FirstAsync();

        foreach (var salary in salaryPerMonth)
        {
            var dataNeeded = usersWithDataNeeded.GetValueOrDefault(salary.UserId);
            
            salary.BaseSalary = dataNeeded!.BaseSalary;
            salary.LateEarlyLeaveCases = dataNeeded.NumberOfLateEarlyCase;
            salary.AbnormalCases = dataNeeded.NumberOfAbnormalCase;
            salary.FinePerAbnormalCase = companyConfig.FinePerAbnormalCase;
            salary.FinePerLateEarlyCase = companyConfig.FinePerLateEarlyCase;
            salary.SalaryPerOvertimeHour = companyConfig.SalaryPerOvertimeHour;
            salary.TotalSalary = salary.BaseSalary
                                 - (salary.FinePerLateEarlyCase * salary.LateEarlyLeaveCases)
                                 - (salary.FinePerAbnormalCase * salary.AbnormalCases)
                                 + (salary.OvertimeHours * salary.SalaryPerOvertimeHour);
            dbContext.Salaries.Update(salary);
        }
        
        await dbContext.SaveChangesAsync();
        
    }
}