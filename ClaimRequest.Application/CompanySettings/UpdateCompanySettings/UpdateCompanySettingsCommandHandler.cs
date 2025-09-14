using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Projects.UpDateProject;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Projects.Errors;
using ClaimRequest.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Domain.CompanySettings.Errors;

namespace ClaimRequest.Application.CompanySettings.UpdateCompanySettings
{
    public class UpdateCompanySettingsCommandHandler(IDbContext context) : ICommandHandler<UpdateCompanySettingsCommand>
    {
        public async Task<Result> Handle(UpdateCompanySettingsCommand command, CancellationToken cancellationToken)
        {
                var CompanySetting = await context.CompanySettings.FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
                if (CompanySetting == null)
                {
                    return Result.Failure(CompanySettingsErrors.NotFound(command.Id));
                }

                if (command.WorkStartTime > command.WorkEndTime)
                {
                    return Result.Failure(CompanySettingsErrors.InvalidTimeRange);
                }

                CompanySetting.LimitDayOff = command.LimitDayOff;
                CompanySetting.WorkStartTime = command.WorkStartTime;
                CompanySetting.WorkEndTime = command.WorkEndTime;
                CompanySetting.FinePerAbnormalCase = command.FinePerAbnormalCase;
                CompanySetting.FinePerLateEarlyCase = command.FinePerLateEarlyCase;
                CompanySetting.SalaryPerOvertimeHour = command.SalaryPerOvertimeHour;
                

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success(CompanySetting);
        }
    }
}