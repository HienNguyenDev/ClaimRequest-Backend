using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.AttendanceRecords.Events;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;


namespace ClaimRequest.Application.Attendance.CheckOut
{
    public class CheckOutCommandHandler(IDbContext dbContext, IUserContext userContext) : ICommandHandler<CheckOutCommand>
    {
        public async Task<Result> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var checkoutTime = DateTime.UtcNow;
            var date = DateOnly.FromDateTime(checkoutTime);
            var user = userContext.UserId;
            var attendance = dbContext.AttendanceRecords.FirstOrDefault(x=>x.WorkDate == date && x.UserId == user);
            var company = dbContext.CompanySettings.AsNoTracking().FirstOrDefault();

            if (attendance == null)
            {
                return Result.Failure(Error.NotFound("Attendance.NotFound", "Attendance not found"));

            }
            if(attendance.CheckInTime == null)
            {
                return Result.Failure(Error.Conflict("CheckInTime.Conflict", $"User {user} has not checked in yet"));
            }
            if (company == null)
            {
                return Result.Failure(Error.NotFound("Company.NotFound", "Company not found"));
            }
            attendance.CheckOutTime = checkoutTime;
            attendance.IsLeaveEarly = TimeOnly.FromDateTime(checkoutTime) < company.WorkEndTime && TimeOnly.FromDateTime(checkoutTime) >= TimeOnly.Parse("8:00");

            attendance.Raise(new CheckOutEvent(attendance.Id));
            await dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success(attendance);
        }
    }
}
