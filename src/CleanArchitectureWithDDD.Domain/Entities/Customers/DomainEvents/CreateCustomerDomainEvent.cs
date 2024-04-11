using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;

public sealed record CreateCustomerDomainEvent(Guid CustomerId) : DomainEvent(Guid.NewGuid())
{
}
