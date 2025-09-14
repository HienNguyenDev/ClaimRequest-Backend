using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims.Errors;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common;
using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.RejectClaim
{
    public class RejectClaimCommandHandler(IDbContext dbContext, IUserContext userContext) : ICommandHandler<RejectClaimCommand, string>
    {
        public async Task<Result<string>> Handle(RejectClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await dbContext.Claims.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (claim == null)
            {
                return Result.Failure<string>(ClaimErrors.NotFound(request.Id));
            }
            if (claim.Status == Domain.Claims.ClaimStatus.Rejected)
            {
                return Result.Failure<string>(ClaimErrors.Rejected(request.Id));
            }

            claim.Status = Domain.Claims.ClaimStatus.Rejected;
            var user = userContext.UserId;
            var claimOwnerUser = claim.UserId;
            var userName = dbContext.Users
                .FirstOrDefault(u => u.Id == user)?
                .FullName; 
            claim.Raise(new ClaimRejectedDomainEvent()
            {
                ClaimId = claim.Id,
                Action = "Reject",
                UserId = user,
                PerformedAt = DateTime.UtcNow
            });

            claim.Raise(new ClaimStatusChangedDomainEvent
            {
                UserId = user,
                ClaimId = claim.Id,
                Action = "Rejected",
                UserActionName = userName ?? "System",
            });
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success($"Claim with {claim.Id} is rejected");
        }

       
    }
}
