using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.CompanySettings.Events
{
    public sealed record CompanySettingCreateDomainEvent(Guid Id) : IDomainEvent;
}

