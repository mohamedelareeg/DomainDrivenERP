using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Customers.DomainEvents;

public sealed record CreateCustomerDomainEvent(Guid CustomerId) : DomainEvent(Guid.NewGuid())
{
}
