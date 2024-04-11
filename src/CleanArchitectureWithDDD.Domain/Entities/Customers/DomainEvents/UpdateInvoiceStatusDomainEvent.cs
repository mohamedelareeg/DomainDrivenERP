using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;

public sealed record UpdateInvoiceStatusDomainEvent(Guid CustomerId, Invoice Invoice, InvoiceStatus InvoiceStatus) : DomainEvent(Guid.NewGuid())
{
}
