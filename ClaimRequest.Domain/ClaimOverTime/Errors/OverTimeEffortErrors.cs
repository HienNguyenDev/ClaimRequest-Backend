using ClaimRequest.Domain.Common;
using ClaimRequest.Domain.Users;


namespace ClaimRequest.Domain.ClaimOverTime.Errors
{
    public class OverTimeEffortErrors
    {
        public static Error NotFound(Guid Id) => Error.NotFound(
        "OverTimeEffort.NotFound",
        $"The over time entry with the Id = '{Id}' was not found");

        public static Error InvalidStatus(OverTimeEffortStatus status, OverTimeEffortStatus correctStatus) => Error.Conflict(
        "OverTimeEffort.InvalidStatus",
        $"The status is {status}. It must be {correctStatus}");
        
        public static Error Rejected(Guid Id) => Error.Conflict(
            "OverTimeEntry.Comflict",
            $"The over time entry with the Id = '{Id}' was rejected");

        public static Error EffortExisted(Guid overTimeRequestId, Guid overTimeMemberId, Guid overTimeDateId) => Error.Conflict(
        "OverTimeEffort.EffortExisted",
        $"The over time effort of member with Id = {overTimeMemberId} " +
            $"and for date with Id = {overTimeDateId} " +
            $"belonging to request with Id {overTimeRequestId} already existed");

        public static Error ActionNotAllowedForThisUserRole(UserRole userRole) => Error.Conflict(
        "OverTimeEffort.ActionNotAllowedForThisUserRole",
        $"You are not the {userRole} of this Over Time Request.");
    }
}
