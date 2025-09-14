using ClaimRequest.Application.Abstraction.Data;
using ClaimRequest.Application.Abstraction.Messaging;
using ClaimRequest.Application.ReasonApplication.GetReasons;
using ClaimRequest.Domain.Common;

namespace ClaimRequest.Application.ReasonApplication.CreateReason
{
    public class CreateReasonCommandHandler(IDbContext context) : ICommandHandler<CreateReasonCommand, ReasonsResponse>
    {
        public async Task<Result<ReasonsResponse>> Handle(CreateReasonCommand request, CancellationToken cancellationToken)
        {
            //if (await context.Reasons.AnyAsync(r => r.Name == request.Name, cancellationToken))
            //{
            //    return Result.Failure<CreateReasonTypeCommandResponse>(ReasonTypeError.NameNotUnique);
            //}

            var reason = new Domain.Reasons.Reason()
            {
                Id = Guid.NewGuid(),
                RequestTypeId = request.RequestTypeId,
                Name = request.Name,
                IsOther = false,
            };
            //reason.Raise(new ReasonCreatedDomainEvent(reason.Id));
            context.Reasons.Add(reason);

            await context.SaveChangesAsync(cancellationToken);

            return new ReasonsResponse
            {
                Id = reason.Id,
                RequestTypeId = reason.RequestTypeId,
                Name = reason.Name,
                IsOther = reason.IsOther,

            };
        }
    }
}
