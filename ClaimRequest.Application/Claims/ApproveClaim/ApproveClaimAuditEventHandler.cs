using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.Claims.Events;
using ClaimRequest.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Claims.ApproveClaim
{
    public class ApproveClaimAuditEventHandler(IDbContext dbContext) : INotificationHandler<ClaimApprovedDomainEvent>
    {


        public async Task Handle(ClaimApprovedDomainEvent notification, CancellationToken cancellationToken)
        {
            var audit = new AuditLog()
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
