using ClaimRequest.Application.Abstraction.Messaging;

namespace ClaimRequest.Application.CompanySettings.UpdateCompanySettings;

    public sealed class UpdateCompanySettingsCommand : ICommand
    {
        public Guid Id { get; set; }
        public int LimitDayOff { get; set; }
        public TimeOnly WorkStartTime { get; set; } 
        public TimeOnly WorkEndTime { get; set; }  
        
        public decimal FinePerLateEarlyCase { get; set; }
        public decimal FinePerAbnormalCase { get; set; }
        public decimal SalaryPerOvertimeHour { get; set; }
        
        
    }