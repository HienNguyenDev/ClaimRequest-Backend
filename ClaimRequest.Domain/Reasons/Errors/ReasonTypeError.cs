using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Domain.Reasons.Errors
{
    public class ReasonTypeError
    {
        public static Error NotFound(Guid reasonTypeId) => Error.NotFound(
        "ReasonType.NotFound",
        $"The Reason Type with the Id = '{reasonTypeId}' was not found");

        public static readonly Error NameNotUnique = Error.Conflict(
            "RequestType.NameNotUnique",
            "This name is already in use");
    }
}
