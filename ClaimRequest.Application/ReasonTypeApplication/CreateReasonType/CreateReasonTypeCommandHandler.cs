using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Domain.Common;
using Microsoft.EntityFrameworkCore;
using ClaimRequest.Domain.Reasons.Errors;
using ClaimRequest.Domain.Reasons.Events;

namespace ClaimRequest.Application.ReasonTypeApplication.CreateReasonType
{
    public class CreateReasonTypeCommandHandler(IDbContext context) : ICommandHandler<CreateReasonTypeCommand, CreateReasonTypeCommandResponse>
    {
        public async Task<Result<CreateReasonTypeCommandResponse>> Handle(CreateReasonTypeCommand request, CancellationToken cancellationToken)
        {
            if (await context.ReasonTypes.AnyAsync(r => r.Name == request.Name,cancellationToken))
            {
                return Result.Failure<CreateReasonTypeCommandResponse>(ReasonTypeError.NameNotUnique);
            }

            var reasonType = new Domain.Reasons.ReasonType()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,

            };

            reasonType.Raise(new ReasonTypeCreatedDomainEvent(reasonType.Id));

            context.ReasonTypes.Add(reasonType);

            var reason = new Domain.Reasons.Reason()
            {
                Id = Guid.NewGuid(),
                Name = "Other",
                RequestTypeId = reasonType.Id,
                IsOther = true,
                IsSoftDeleted = 0,
            };
            context.Reasons.Add(reason);

            await context.SaveChangesAsync(cancellationToken);

            return new CreateReasonTypeCommandResponse{
                Id = reasonType.Id,
                Name = reasonType.Name

            };
        }
    }
    
}
