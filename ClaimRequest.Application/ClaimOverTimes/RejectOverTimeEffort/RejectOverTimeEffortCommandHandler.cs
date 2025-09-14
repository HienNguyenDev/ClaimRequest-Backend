using System.Data;
using ClaimRequest.Application.Abstraction.Authentication;
using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.ClaimOverTime;
using ClaimRequest.Domain.ClaimOverTime.Errors;
using ClaimRequest.Domain.ClaimOverTime.Events;
using ClaimRequest.Domain.Common;
using DocumentFormat.OpenXml.Drawing;
using Microsoft.EntityFrameworkCore;

namespace ClaimRequest.Application.ClaimOverTimes.RejectOverTimeEffort;

public class RejectOverTimeEffortCommandHandler(IDbContext dbContext, IUserContext userContext) : ICommandHandler<RejectOverTimeEffortCommand, string>
{
    public async Task<Result<string>> Handle(RejectOverTimeEffortCommand request, CancellationToken cancellationToken)
    {
        
        var overTimeEffort = await dbContext.OverTimeEffort.FirstOrDefaultAsync(x=>x.Id == request.Id, cancellationToken);
        
        if (overTimeEffort == null)
        {
            return Result.Failure<string>(OverTimeEffortErrors.NotFound(request.Id));
        }

        if (overTimeEffort.Status == OverTimeEffortStatus.Rejected)
        {
            return Result.Failure<string>(OverTimeEffortErrors.Rejected(request.Id));
        }
        overTimeEffort.Status = OverTimeEffortStatus.Rejected;
        overTimeEffort.Raise(new OverTimeEffortStatusChangedEvent
        {
            EffortId = overTimeEffort.Id,
            Action = "Rejected",
            UserId = userContext.UserId
        });
        await dbContext.SaveChangesAsync(cancellationToken);
         
         return  Result.Success($"OverTimeEffort with {overTimeEffort.Id} was rejected.");
    }
}