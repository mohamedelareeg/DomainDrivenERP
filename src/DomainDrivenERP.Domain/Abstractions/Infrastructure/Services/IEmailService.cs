using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Enums;

namespace DomainDrivenERP.Domain.Abstractions.Infrastructure.Services;

public interface IEmailService
{
    Task SendInvoiceToCustomerViaEmailAsync(Customer customer, Invoice invoice, CancellationToken cancellationToken = default);
    Task SendInvoiceStatusUpdateToCustomerViaEmailAsync(Customer customer, Invoice invoice, InvoiceStatus invoiceStatus, CancellationToken cancellationToken = default);
    Task SendCustomerCreationViaEmailAsync(Task<Customer> customer, CancellationToken cancellationToken);
}
