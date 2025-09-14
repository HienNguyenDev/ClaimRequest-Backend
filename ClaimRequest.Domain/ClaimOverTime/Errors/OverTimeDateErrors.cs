using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.ClaimOverTime.Errors
{
    public class OverTimeDateErrors
    {
        public static Error NotFound(Guid Id) => Error.NotFound(
        "OverTimeDate.NotFound",
        $"The over time date with the Id = '{Id}' was not found");
    }
}
