using ClaimRequest.Domain.Common;
using System.Diagnostics;

namespace ClaimRequest.Domain.Claims.Errors;

public class ClaimErrors
{
    public static readonly Error AddClaimToLate = Error.Conflict(
        "Claims.AddClaimToLate",
        "The compensate must be create before the 5rd of the next month of day off.");

    public static readonly Error TooFarDate = Error.Conflict(
        "Claims.TooFarDate",
        "The claim for future cannot be in next year");

    public static readonly Error InvalidApprover = Error.Conflict(
        "Claims.InvalidApprover",
        "The approver for the claim is invalid");

    public static readonly Error InvalidSupervisor = Error.Conflict(
        "Claims.InvalidSupervisor",
        "The supervisor for the claim is invalid");
    public static Error NotApproved(Guid Id) => Error.Conflict(
         "Claim.Problem",
         $"The claim with the '{Id}' hasn't approved");
    public static Error Paided(Guid Id) => Error.Conflict(
        "Claim.Paided",
        $"The claim with the '{Id}' was paided");
    public static Error NotFound(Guid Id) => Error.NotFound(
        "Claims.NotFound",
        $"The claim with the Id = '{Id}' was not found");
    public static Error InvalidStatus(ClaimStatus status) => Error.Conflict(
        "Claims.InvalidStatus",
        $"The status is {status}. It must be Pending");

    public static readonly Error ProcessRequired = Error.Conflict(
        "ReturnClaim.ConfirmationRequired",
        "The claim has not been processed and cannot be returned.");

    public static readonly Error ApproveRequired = Error.Conflict(
        "ReturnClaim.AlreadyApproved",
        "User must Approve before proceeding with returning the claim.");
    //public static Error Approved(Guid Id) => Error.Conflict(
    //    "Claim.Problem",
    //    $"The claim with the {Id} was approved");
    public static Error InValidStatusForApproveClaim(ClaimStatus status) => Error.Conflict(
        "Claim.InValidStatusForApproveClaim",
        $"The status is {status}. It must be Confirmed"
        );
    public static Error InvalidStatusToUpdate(ClaimStatus status, ClaimStatus requiredStatus) => Error.Conflict(
    "Claims.InvalidStatus",
    $"The status is {status}. It must be {requiredStatus}");

    public static Error Rejected(Guid Id) => Error.Conflict(
        "Claim.Rejected",
        $"The claim with the '{Id}' was rejected");
    public static Error InValidStatusForCancelClaim(ClaimStatus status) => Error.Conflict(
        "Claim.InValidStatusForCancelClaim",
        $"The status is {status}. It must not be Cancel or Draft"
        );
    public static Error InValidStatusForReturnClaim(ClaimStatus status) => Error.Conflict(
        "Claim.InValidStatusForReturnClaim",
        $"The status is {status}. It must be Pending"
        );
    
    public static Error ClaimExistedForThisAbnormalCase() => Error.Conflict(
        "Claim.ClaimExistedForThisAbnormalCase",
        $"There is existed claim for this abnormal case"
    );
    public static Error ClaimExistedForThisLateEarlyCase() => Error.Conflict(
        "Claim.ClaimExistedForThisLateEarlyCase",
        $"There is existed claim for this late early case"
    );
    
    public static Error NoAbnormalCaseOrLateEarlyCaseForThisDay() => Error.Conflict(
        "Claim.ClaimExistedForThisLateEarlyCase",
        $"There is no abnormal case or late early case for this day"
    );
    public static Error NotFoundByEmail() => Error.NotFound(
        "Claims.NotFound",
        $"The claim with that Email was not found");
    public static Error InvalidEmail() => Error.Conflict(
        "Claims.InvalidEmail",
        "The Email is invalid");
}