using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.DomainEvents;

public abstract record DomainEvent(Guid Id): IDomainEvent;
