using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.AttendanceRecords;
using ClaimRequest.Domain.AttendanceRecords.Events;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Attendance.CheckIn
{
    public class CheckInCommandHandler(IDbContext dbContext, IUserContext userContext) : ICommandHandler<CheckInCommand>
    {
        public async Task<Result> Handle(CheckInCommand request, CancellationToken cancellationToken)
        {
            var checkinTime = DateTime.UtcNow;
            var date =DateOnly.FromDateTime( checkinTime);
            var user = userContext.UserId;
            var attendance =  dbContext.AttendanceRecords.FirstOrDefault(x=>x.WorkDate == date && x.UserId == user  );
            var company = dbContext.CompanySettings.AsNoTracking().FirstOrDefault();
            if(company == null)
            {
                return Result.Failure(Error.NotFound("Company.NotFound","Company not found"));
            }
            
            if (attendance == null)
            {
               
             return Result.Failure(Error.NotFound("Attendance.NotFound", "Attendance record not found"));

            }
        
            if (attendance.CheckInTime != null)
            {
                    return Result.Failure(Error.Conflict("CheckInTime.Conflict","Check-in-time is existed"));
            }
            else
            {
                    attendance.CheckInTime = checkinTime;
                    
            }
            attendance.IsLateCome = TimeOnly.FromDateTime(checkinTime) > company.WorkStartTime && TimeOnly.FromDateTime(checkinTime) <= TimeOnly.Parse("03:00");
               
            attendance.Raise(new CheckInEvent(attendance.Id));
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success(attendance);  
        }
    }
}
