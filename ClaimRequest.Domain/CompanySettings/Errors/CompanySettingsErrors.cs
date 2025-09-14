using System.Runtime.InteropServices.JavaScript;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.CompanySettings.Errors;

public static class CompanySettingsErrors
{
    public static Error CodeNotUnique = Error.Conflict(
        "CompanySettings.CodeNotUnique",
        "The provided code is not unique");
    
    public static Error NotFound(Guid? Id) => Error.NotFound(
        "CompanySettings.NotFound",
        $"The user with the Id = '{Id}' was not found");

    public static readonly Error InvalidTimeRange = Error.Conflict(
        "UpdateCompanySettings.InvalidTimeRange",
        "WorkStartTime must be before to WorkEndTime");
    
    
}