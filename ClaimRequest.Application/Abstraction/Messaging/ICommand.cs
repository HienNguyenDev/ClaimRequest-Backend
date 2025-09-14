using ClaimRequest.Domain.Common;
using MediatR;

namespace ClaimRequest.Application.Abstraction.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;