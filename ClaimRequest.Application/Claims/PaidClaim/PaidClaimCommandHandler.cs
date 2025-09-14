using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Claims.Errors;
using ClaimRequest.Domain.Claims;
using ClaimRequest.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Application.Abstraction.Authentication;
using DocumentFormat.OpenXml.InkML;

namespace ClaimRequest.Application.Claims.PaidClaim
{
    public class PaidClaimCommandHandler : ICommandHandler<PaidClaimCommand>
    {
        private readonly IDbContext _context;
        private readonly IUserContext _userContext;
        public PaidClaimCommandHandler(IDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result> Handle(PaidClaimCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var claim = await _context.Claims.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
                if (claim == null)
                {
                    return Result.Failure(ClaimErrors.NotFound(request.Id));
                }
                else if (claim.Status == ClaimStatus.Paid)
                {
                    return Result.Failure(ClaimErrors.Paided(request.Id));
                } 
                else if (claim.Status != ClaimStatus.Approved)
                {
                    return Result.Failure(ClaimErrors.NotApproved(request.Id));
                }
                else if (claim.Status == ClaimStatus.Approved)
                {
                    claim.Status = ClaimStatus.Paid;
                }

                var user = _userContext.UserId;
                var claimOwnerUser = claim.UserId;
                var userName = _context.Users
                    .FirstOrDefault(u => u.Id == user)?
                    .FullName;
                claim.Raise(new ClaimStatusChangedDomainEvent
                {
                    UserId = claimOwnerUser,
                    ClaimId = request.Id,
                    Action = "Paided",
                    UserActionName = userName ?? "System",
                });
                
                if (claim.ClaimFee != 0)
                {
                    claim.Raise(new ClaimHasFeePaidEvent
                    {
                        ClaimId = claim.Id,
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success(claim);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
