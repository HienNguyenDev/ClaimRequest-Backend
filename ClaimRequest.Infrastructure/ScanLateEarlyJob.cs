using ClaimRequest.Domain.LateEarlyCases;
using ClaimRequest.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Quartz;
namespace ClaimRequest.Infrastructure;

[DisallowConcurrentExecution]
public class ScanLateEarlyJob(ApplicationDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        // get nhung attendance cua hom nay nhung IsLate hoac IsEarly = true
        // tao late early cases
        
        DateOnly workDate = DateOnly.FromDateTime(DateTime.UtcNow);
        
        if (workDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return;
        }
        
        DateTime tenAM = DateTime.SpecifyKind(workDate.ToDateTime(new TimeOnly(3, 0)), DateTimeKind.Utc);
        DateTime threePM = DateTime.SpecifyKind(workDate.ToDateTime(new TimeOnly(8, 0)), DateTimeKind.Utc);
        
        var attendances = dbContext.AttendanceRecords
            .Where(a => a.WorkDate == workDate);
        
        var lateOrEarly = attendances
            .Where(a => (a.IsLateCome == true && a.CheckOutTime > tenAM) || (a.IsLeaveEarly == true && a.CheckInTime < threePM));
        
        var settings = dbContext.CompanySettings.AsNoTracking().FirstOrDefault();

        DateTime now = DateTime.UtcNow;

        var checkInTime = new DateTime(now.Year, now.Month, now.Day, settings.WorkStartTime.Hour
            , settings.WorkStartTime.Minute, settings.WorkStartTime.Second, DateTimeKind.Utc);

        var checkOutTime = new DateTime(now.Year, now.Month, now.Day, settings.WorkEndTime.Hour
            , settings.WorkEndTime.Minute, settings.WorkEndTime.Second, DateTimeKind.Utc);
        
        foreach (var record in lateOrEarly)
        {
            var lateOrEarlyCase = new LateEarlyCase
            {
                Id = Guid.NewGuid(),
                WorkDate = workDate,
                IsLateCome = record.IsLateCome,
                IsEarlyLeave = record.IsLeaveEarly,
                CheckInTime = record.CheckInTime!.Value,
                CheckoutTime = record.CheckOutTime!.Value,
                LateTimeSpan = record.IsLateCome ? record.CheckInTime.Value - checkInTime : TimeSpan.Zero,
                EarlyTimeSpan = record.IsLeaveEarly ? checkOutTime - record.CheckOutTime.Value : TimeSpan.Zero,
                UserId = record.UserId,
            };
            dbContext.LateEarlyCases.Add(lateOrEarlyCase);
        }
        await dbContext.SaveChangesAsync();
    }
}