using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{

}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
