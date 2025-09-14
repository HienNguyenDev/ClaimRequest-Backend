using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.Claims.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.RejectClaim
{
    public class RejectClaimAuditEventHandler(IDbContext dbContext) : INotificationHandler<ClaimRejectedDomainEvent>
    {
        public async Task Handle(ClaimRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            var audit = new AuditLog
            {
                Id = Guid.NewGuid(),
                Action = notification.Action,
                ClaimId = notification.ClaimId,
                PerformedAt = notification.PerformedAt,
                UserId = notification.UserId,
            };

            dbContext.AuditLogs.Add(audit);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            
            
        }
    }
}
