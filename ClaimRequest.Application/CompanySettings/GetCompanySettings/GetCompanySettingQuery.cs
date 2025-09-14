using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.Abstraction.Query;
using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.CompanySettings.GetCompanySettings
{
    public class GetCompanySettingQuery : IQuery<GetCompanySettingQueryResponse>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
    }
}
