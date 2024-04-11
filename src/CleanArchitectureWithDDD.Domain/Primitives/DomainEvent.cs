namespace CleanArchitectureWithDDD.Domain.Primitives;

public abstract record DomainEvent(Guid Id) : IDomainEvent;
