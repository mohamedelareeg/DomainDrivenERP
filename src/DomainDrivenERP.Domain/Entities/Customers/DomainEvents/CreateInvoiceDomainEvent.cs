using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Customers.DomainEvents;

// Record is Immutable
public sealed record CreateInvoiceDomainEvent(Guid CustomerId, Invoice Invoice) : DomainEvent(Guid.NewGuid())
{
}
