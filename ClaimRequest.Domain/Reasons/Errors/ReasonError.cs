using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Domain.Reasons.Errors
{
    public class ReasonError
    {
        public static Error NotFound(Guid reasonId) => Error.NotFound(
        "Reason.NotFound",
        $"The Reason with the Id = '{reasonId}' was not found");
    }
}
