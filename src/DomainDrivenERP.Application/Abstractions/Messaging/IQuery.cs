using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
