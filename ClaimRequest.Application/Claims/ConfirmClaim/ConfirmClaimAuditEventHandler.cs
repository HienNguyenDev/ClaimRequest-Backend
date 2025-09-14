using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Domain.AuditLogs;
using ClaimRequest.Domain.Claims.Events;
using MediatR;

namespace ClaimRequest.Application.Claims.ConfirmClaim;

public class ConfirmClaimAuditEventHandler
    (IDbContext dbContext): INotificationHandler<ClaimConfirmedDomainEvent>
{
    public async Task Handle(ClaimConfirmedDomainEvent notification, CancellationToken cancellationToken)
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