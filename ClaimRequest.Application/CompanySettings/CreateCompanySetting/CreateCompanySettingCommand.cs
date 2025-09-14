using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.CompanySettings.CreateCompanySetting;

public sealed class CreateCompanySettingCommand : ICommand<CreateCompanySettingCommandResponse>
{
    public Guid Id { get;  init; }
    public int LimitDayOff { get; init; }
    public TimeOnly WorkStartTime { get; init; } 
    public TimeOnly WorkEndTime { get; init; }
    
    public decimal FinePerLateEarlyCase { get; set; }
    public decimal FinePerAbnormalCase { get; set; }
    public decimal SalaryPerOvertimeHour { get; set; }
    
}