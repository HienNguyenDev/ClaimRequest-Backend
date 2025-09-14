using ClaimRequest.Domain.Common;
using MediatR;

namespace ClaimRequest.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;