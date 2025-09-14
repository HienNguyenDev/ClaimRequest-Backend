using ClaimRequest.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Quartz;
using System.Data.SqlClient;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Infrastructure.Database;

namespace ClaimRequest.Infrastructure;
[DisallowConcurrentExecution]

public class CreateAttendanceRecordJob(ApplicationDbContext dbContext) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
      
        DateOnly workDate = DateOnly.FromDateTime(DateTime.UtcNow);

        if (workDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
        {
            return;
        }
        
        var attendances = dbContext.Users.Select(user => new AttendanceRecord
        {
            UserId = user.Id,
            WorkDate = workDate,
        }).ToList();
        dbContext.AttendanceRecords.AddRange(attendances);
        await dbContext.SaveChangesAsync();
    }
}