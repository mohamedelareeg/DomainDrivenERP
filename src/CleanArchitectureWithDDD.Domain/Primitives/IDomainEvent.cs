using MediatR;

namespace CleanArchitectureWithDDD.Domain.Primitives;

//Outbox Pattern
//Domain Event Pattern
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
