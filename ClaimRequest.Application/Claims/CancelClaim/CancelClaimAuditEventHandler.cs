using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.Claims.Events;
using MediatR;

namespace ClaimRequest.Application.Claims.CancelClaim
{
    public class CancelClaimAuditEventHandler(IDbContext dbContext) : INotificationHandler<ClaimCanceledDomainEvent>
    {
        public async Task Handle(ClaimCanceledDomainEvent notification, CancellationToken cancellationToken)
        {
            var audit = new AuditLog
            {
                Id = Guid.NewGuid(),
                ClaimId = notification.ClaimId,
                UserId = notification.UserId,
                PerformedAt = notification.PerformedAt,
                Action = notification.Action,
            };

            dbContext.AuditLogs.Add(audit);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
