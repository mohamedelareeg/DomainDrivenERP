using CleanArchitectureWithDDD.Domain.Primitives;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
where TEvent : IDomainEvent
{
}
