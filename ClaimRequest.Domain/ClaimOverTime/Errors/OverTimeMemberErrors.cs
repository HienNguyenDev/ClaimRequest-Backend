using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.ClaimOverTime.Errors
{
    public class OverTimeMemberErrors
    {
        public static Error NotFound(Guid Id) => Error.NotFound(
        "OverTimeMember.NotFound",
        $"The over time member with the Id = '{Id}' was not found");
    }
}
