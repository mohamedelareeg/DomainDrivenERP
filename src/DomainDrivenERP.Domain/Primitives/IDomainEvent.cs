using MediatR;

namespace DomainDrivenERP.Domain.Primitives;

// Outbox Pattern
// Domain Event Pattern
public interface IDomainEvent : INotification
{
    Guid Id { get; }
}
