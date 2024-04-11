using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;

// Record is Immutable
public sealed record CreateInvoiceDomainEvent(Guid CustomerId, Invoice Invoice) : DomainEvent(Guid.NewGuid())
{
}
