using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Customers.DomainEvents;

public sealed record UpdateInvoiceStatusDomainEvent(Guid CustomerId, Invoice Invoice, InvoiceStatus InvoiceStatus) : DomainEvent(Guid.NewGuid())
{
}
