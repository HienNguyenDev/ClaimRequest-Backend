using ClaimRequest.Domain.AbnormalCases;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ClaimRequest.Infrastructure;
[DisallowConcurrentExecution]

public class ScanAbnormalCaseJob(ApplicationDbContext dbContext) : IJob 
{
    public async Task Execute(IJobExecutionContext context)
    {
      
      // User khong check in, khong check out
      DateOnly workDate = DateOnly.FromDateTime(DateTime.UtcNow);
      
      if (workDate.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
      {
          return;
      }
      
      var offAllDayCases = await dbContext.AttendanceRecords.Where(a => a.WorkDate == workDate 
                                                                        && a.CheckInTime == null 
                                                                        && a.CheckOutTime == null).ToListAsync(); 
      
      var offAllDayUserIds = offAllDayCases.Select(a => a.UserId).ToList();
      
      var offAllDayUserWithClaims = await dbContext.Claims.Where(c => c.Status == ClaimStatus.Approved
                                                                      && offAllDayUserIds.Contains(c.UserId) && c.ClaimDetails.Any(cd => cd.Date == workDate))
          .GroupBy(c => c.UserId)
          .ToDictionaryAsync(g => g.Key, g => g.ToList());
      
      var offAllDayWithPermissionUserIds =
          offAllDayUserWithClaims.Where(c =>
              c.Value.Count == 2 || (c.Value.Count == 1 && c.Value.First().Partial == Partial.AllDay)).Select(c => c.Key).ToList();

      var offAfternoonWithoutPermissionUserIds1 = offAllDayUserWithClaims
          .Where(c => !offAllDayWithPermissionUserIds.Contains(c.Key) && c.Value.Count == 1 && c.Value.First().Partial == Partial.Morning)
          .Select(c => c.Key).ToList();
      
      
      
      var offMorningWithoutPermissionUserIds1 = offAllDayUserWithClaims
          .Where(c => !offAllDayWithPermissionUserIds.Contains(c.Key) && c.Value.Count == 1 && c.Value.First().Partial == Partial.Afternoon)
          .Select(c => c.Key).ToList();
      
      var offAllDayWithoutPermissionUserIds1 = offAllDayUserIds.Where(c => !offAllDayWithPermissionUserIds.Contains(c) 
                                                                           && !offAfternoonWithoutPermissionUserIds1.Contains(c) 
                                                                           && !offMorningWithoutPermissionUserIds1.Contains(c)).ToList();
      
      
      var allDayAbnormalCase1 = offAllDayWithoutPermissionUserIds1.Select(x => new AbnormalCase
      {
          Id = Guid.NewGuid(),
          WorkDate = workDate,
          UserId = x,
          AbnormalType = AbnormalType.OffAllDayWithoutPermission
      });
      
      var offMorningAbnormalCase1 = offMorningWithoutPermissionUserIds1.Select(x => new AbnormalCase
      {
          Id = Guid.NewGuid(),
          WorkDate = workDate,
          UserId = x,
          AbnormalType = AbnormalType.OffMorningWithoutPermission
      });
      
      var offAfternoonAbnormalCase1 = offAfternoonWithoutPermissionUserIds1.Select(x => new AbnormalCase
      {
          Id = Guid.NewGuid(),
          WorkDate = workDate,
          UserId = x,
          AbnormalType = AbnormalType.OffAfternoonWithoutPermission
      });
      
      dbContext.AbnormalCases.AddRange(allDayAbnormalCase1);
      dbContext.AbnormalCases.AddRange(offMorningAbnormalCase1);
      dbContext.AbnormalCases.AddRange(offAfternoonAbnormalCase1);
      
      // User co check in, co check out
      DateTime tenAM = workDate.ToDateTime(new TimeOnly(3, 0)); 
      DateTime threePM = workDate.ToDateTime(new TimeOnly(8, 0)); 
      
      var attendances = await dbContext.AttendanceRecords.Where(a => a.WorkDate == workDate 
                                                                     && a.CheckInTime != null 
                                                                     && a.CheckOutTime != null).ToListAsync();
      
      
      var offMorningUserIds = attendances.Where(a => a.CheckInTime > tenAM || a.CheckOutTime < tenAM)
          .Select(a => a.UserId).ToList(); 
      
      var offMorningWithPermissionUserIds = await dbContext.ClaimDetails
          .Where(cd => cd.Date == workDate 
          && (cd.Claim.Partial == Partial.Morning || cd.Claim.Partial == Partial.AllDay)
          && cd.Claim.Status == ClaimStatus.Approved
          && offMorningUserIds.Contains(cd.Claim.UserId))
          .Select(cd => cd.Claim.UserId).ToListAsync();

      var offMorningWithoutPermissionUserIds2 = offMorningUserIds.Where(a => !offMorningWithPermissionUserIds.Contains(a)).ToList();
      
      
      var offAfternoonUserIds = attendances.Where(a => a.CheckInTime > threePM || a.CheckOutTime < threePM)
          .Select(a => a.UserId).ToList();
      
      var offAfternoonWithPermissionUserIds = await dbContext.ClaimDetails
          .Where(cd => cd.Date == workDate 
                       && (cd.Claim.Partial == Partial.Afternoon || cd.Claim.Partial == Partial.AllDay) 
                       && cd.Claim.Status == ClaimStatus.Approved
                       && offAfternoonUserIds.Contains(cd.Claim.UserId))
          .Select(cd => cd.Claim.UserId).ToListAsync();
      
      var offAfternoonWithoutPermissionUserIds2 = offAfternoonUserIds.Where(a => !offAfternoonWithPermissionUserIds.Contains(a)).ToList();
      
      var offAllDayWithoutPermissionUserIds2 = offMorningWithoutPermissionUserIds2.Intersect(offAfternoonWithoutPermissionUserIds2).ToList();

      offMorningWithoutPermissionUserIds2.RemoveAll(userId => offAllDayWithoutPermissionUserIds2.Contains(userId));
      offAfternoonWithoutPermissionUserIds2.RemoveAll(userId => offAllDayWithoutPermissionUserIds2.Contains(userId));
      
      var allDayAbnormalCase2 = offAllDayWithoutPermissionUserIds2.Select(x => new AbnormalCase
      {
          Id = Guid.NewGuid(),
          WorkDate = workDate,
          UserId = x,
          AbnormalType = AbnormalType.OffAllDayWithoutPermission
      });
      
      var offMorningAbnormalCase2 = offMorningWithoutPermissionUserIds2.Select(x => new AbnormalCase
      {
          Id = Guid.NewGuid(),
          WorkDate = workDate,
          UserId = x,
          AbnormalType = AbnormalType.OffMorningWithoutPermission
      });
      
      var offAfternoonAbnormalCase2 = offAfternoonWithoutPermissionUserIds2.Select(x => new AbnormalCase
      {
          Id = Guid.NewGuid(),
          WorkDate = workDate,
          UserId = x,
          AbnormalType = AbnormalType.OffAfternoonWithoutPermission
      });
      
      dbContext.AbnormalCases.AddRange(allDayAbnormalCase2);
      dbContext.AbnormalCases.AddRange(offMorningAbnormalCase2);
      dbContext.AbnormalCases.AddRange(offAfternoonAbnormalCase2);
      
      await dbContext.SaveChangesAsync();
    }
}