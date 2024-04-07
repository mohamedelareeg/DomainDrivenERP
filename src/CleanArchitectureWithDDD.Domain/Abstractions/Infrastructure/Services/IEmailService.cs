using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Enums;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;

public interface IEmailService
{
    Task SendInvoiceToCustomerViaEmailAsync(Customer customer, Invoice invoice, CancellationToken cancellationToken = default);
    Task SendInvoiceStatusUpdateToCustomerViaEmailAsync(Customer customer, Invoice invoice, InvoiceStatus invoiceStatus, CancellationToken cancellationToken = default);
    Task SendCustomerCreationViaEmailAsync(Task<Customer> customer, CancellationToken cancellationToken);
}
