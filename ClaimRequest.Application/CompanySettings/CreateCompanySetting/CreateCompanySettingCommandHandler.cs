using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.CompanySettings;
using ClaimRequest.Domain.CompanySettings.Events;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.CompanySettings.CreateCompanySetting;

public class CreateCompanySettingCommandHandler(
    IDbContext context,
    IPasswordHasher passwordHasher
) : ICommandHandler<CreateCompanySettingCommand, CreateCompanySettingCommandResponse>
{
    public async Task<Result<CreateCompanySettingCommandResponse>> Handle(CreateCompanySettingCommand command,
        CancellationToken cancellationToken)
    {
        var companySetting = await context.CompanySettings.FirstOrDefaultAsync(cancellationToken);

        if (companySetting != null)
        {
            companySetting.LimitDayOff = command.LimitDayOff;
            companySetting.WorkStartTime = command.WorkStartTime;
            companySetting.WorkEndTime = command.WorkEndTime;
            companySetting.FinePerAbnormalCase = command.FinePerAbnormalCase;
            companySetting.SalaryPerOvertimeHour = command.SalaryPerOvertimeHour;
            companySetting.FinePerLateEarlyCase = command.FinePerLateEarlyCase;
        }
        else
        {
            companySetting = new CompanySetting
            {
                Id = Guid.NewGuid(),
                LimitDayOff = command.LimitDayOff,
                WorkStartTime = command.WorkStartTime,
                WorkEndTime = command.WorkEndTime,
                FinePerAbnormalCase = command.FinePerAbnormalCase,
                SalaryPerOvertimeHour = command.SalaryPerOvertimeHour,
                FinePerLateEarlyCase = command.FinePerLateEarlyCase,
            };
            context.CompanySettings.Add(companySetting);
        }
        
        companySetting.Raise(new CompanySettingCreateDomainEvent(companySetting.Id));
        await context.SaveChangesAsync(cancellationToken);

        var companySettingResponse = new CreateCompanySettingCommandResponse
        {
            LimitDayOff = companySetting.LimitDayOff,
            WorkStartTime = companySetting.WorkStartTime,
            WorkEndTime = companySetting.WorkEndTime,
            FinePerAbnormalCase = companySetting.FinePerAbnormalCase,
            SalaryPerOvertimeHour = companySetting.SalaryPerOvertimeHour,
            FinePerLateEarlyCase = companySetting.FinePerLateEarlyCase,
            
            
        };

        return companySettingResponse;
    }
}