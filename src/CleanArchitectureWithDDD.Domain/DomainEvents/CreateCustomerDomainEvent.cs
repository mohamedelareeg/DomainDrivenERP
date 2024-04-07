namespace CleanArchitectureWithDDD.Domain.DomainEvents;

public sealed record CreateCustomerDomainEvent(Guid Id, Guid CustomerId): DomainEvent(Id)
{
}
