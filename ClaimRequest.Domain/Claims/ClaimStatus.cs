namespace ClaimRequest.Domain.Claims;

public enum ClaimStatus
{
    Draft = 0,
    Pending = 1,
    Confirmed = 2,
    Approved = 3,
    Rejected = 4,
    Returned = 5,
    Paid = 6,
    Cancel = 7
}