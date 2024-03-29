using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendInvoiceToCustomerViaEmailAsync(Customer customer , Invoice invoice, CancellationToken cancellationToken = default);
        Task SendInvoiceStatusUpdateToCustomerViaEmailAsync(Customer customer, Invoice invoice , InvoiceStatus invoiceStatus, CancellationToken cancellationToken = default);
        Task SendCustomerCreationViaEmailAsync(Task<Customer> customer, CancellationToken cancellationToken);
    }
}
