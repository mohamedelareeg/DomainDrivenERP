using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{

}
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}
