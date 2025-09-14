using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Extensions;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.CompanySettings.GetCompanySettings
{
    public class GetCompanySettingQueryHandler(IDbContext dbContext) : IQueryHandler<GetCompanySettingQuery, GetCompanySettingQueryResponse>
    {
        public async Task<Result<GetCompanySettingQueryResponse>> Handle(GetCompanySettingQuery request, CancellationToken cancellationToken)
        {
            var companySetting = await dbContext.CompanySettings
                .Select(cs => new GetCompanySettingQueryResponse
                {
                    Id = cs.Id,
                    LimitDayOff = cs.LimitDayOff,
                    WorkStartTime = cs.WorkStartTime,
                    WorkEndTime = cs.WorkEndTime,
                    FinePerAbnormalCase = cs.FinePerAbnormalCase,
                    SalaryPerOvertimeHour = cs.SalaryPerOvertimeHour,
                    FinePerLateEarlyCase = cs.FinePerLateEarlyCase
                })
                .FirstOrDefaultAsync(cancellationToken);

            return companySetting;
        }

    }
}
