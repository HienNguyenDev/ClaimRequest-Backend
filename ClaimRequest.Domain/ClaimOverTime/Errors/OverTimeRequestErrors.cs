using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;

namespace ClaimRequest.Domain.ClaimOverTime.Errors;

public class OverTimeRequestErrors
{
    public static Error RequestExisted(DateOnly date, Guid projectId) => Error.Conflict(
        "OverTimeRequests.RequestExisted",
        $"The over time request is already existed for project with Id = '{projectId}' on {date}.");
    
    public static readonly Error InvalidApprover = Error.Conflict(
        "OverTimeRequest.InvalidApprover",
        "The approver for the over time request is invalid");
    
    public static Error NotFound(Guid Id) => Error.NotFound(
        "OverTimeRequest.NotFound",
        $"The over time request with the Id = '{Id}' was not found");
    public static Error InvalidStartDate() => Error.Conflict(
        "OverTimeRequest.InvalidStartDate",
        $"Overtime start date must be greater than today's date");

    public static Error InvalidEndDate() => Error.Conflict(
        "OverTimeRequest.InvalidEndDate",
        $"Overtime end date must be less than or equal today's date");

    public static Error InvalidExpiryDate() => Error.Conflict(
        "OverTimeRequest.InvalidExpiryDate",
        $"Overtime ExpiresAt must be greater than start date and less than end date");

    public static Error InvalidOverTimeDates() => Error.Conflict(
        "OverTimeRequest.InvalidOverTimeDates",
        $"All overtime dates must be within the range of the start and end dates.");

    public static Error DuplicateOverTimeDates() => Error.Conflict(
        "OverTimeRequest.DuplicateOverTimeDates",
        $"All overtime dates must be unique.");

    public static Error InvalidStatus(OverTimeRequestStatus status, OverTimeRequestStatus correctStatus) => Error.Conflict(
        "OverTimeRequest.InvalidStatus",
        $"The status is {status}. It must be {correctStatus}");

    public static Error ActionNotAllowedForThisUserRole(UserRole userRole) => Error.Conflict(
        "OverTimeRequest.ActionNotAllowedForThisUserRole",
        $"You are not the {userRole} of this Over Time Request.");

    public static Error ExpiredDate(DateOnly endDate) => Error.Conflict(
        "OverTimeRequest.ExpiredDate",
        $"The overtime request end date is {endDate}.");
   
    public static Error Rejected(Guid Id) => Error.Conflict(
        "OverTimeRequest.Rejected",
        $"The over time request with the Id = '{Id}' was already rejected");

}