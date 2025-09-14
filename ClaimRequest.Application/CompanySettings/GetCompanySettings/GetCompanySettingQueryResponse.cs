using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.CompanySettings.GetCompanySettings
{
    public sealed record class GetCompanySettingQueryResponse
    {
        public Guid Id { get; set; }
        public int LimitDayOff { get; set; }
        public TimeOnly WorkStartTime { get; set; }
        public TimeOnly WorkEndTime { get; set; }
        
        public decimal FinePerLateEarlyCase { get; set; }
        public decimal FinePerAbnormalCase { get; set; }
        public decimal SalaryPerOvertimeHour { get; set; }
        
        
    }
}
