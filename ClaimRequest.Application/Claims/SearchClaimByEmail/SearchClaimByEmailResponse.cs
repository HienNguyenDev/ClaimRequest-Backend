using ClaimRequest.Application.Claims.SearchClaimByEmail;
using ClaimRequest.Domain.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.SearchClaimByEmail
{
    public class SearchClaimByEmailResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ReasonId { get; set; }
        public string? OtherReasonText { get; set; }
        public ClaimStatus Status { get; set; }
        public byte IsSoftDeleted { get; set; }
        public Guid SupervisorId { get; set; }
        public Guid ApproverId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Partial Partial { get; set; }
    }

}
