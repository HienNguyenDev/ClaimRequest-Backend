using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.CompanySettings;

public class CompanySetting : Entity
{
    public Guid Id { get;  set; }
    public int LimitDayOff { get; set; }
    public TimeOnly WorkStartTime { get; set; } 
    public TimeOnly WorkEndTime { get; set; }  
    
    public decimal FinePerLateEarlyCase { get; set; }
    public decimal FinePerAbnormalCase { get; set; }
    public decimal SalaryPerOvertimeHour { get; set; }
}