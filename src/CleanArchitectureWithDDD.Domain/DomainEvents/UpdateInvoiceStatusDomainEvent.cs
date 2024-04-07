using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Enums;

namespace CleanArchitectureWithDDD.Domain.DomainEvents;

public sealed record UpdateInvoiceStatusDomainEvent(Guid Id, Guid CustomerId, Invoice Invoice, InvoiceStatus InvoiceStatus) : DomainEvent(Id)
{
}
