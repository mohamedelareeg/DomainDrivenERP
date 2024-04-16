using DomainDrivenERP.Domain.Primitives;
using MediatR;

namespace DomainDrivenERP.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
where TEvent : IDomainEvent
{
}
