using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
