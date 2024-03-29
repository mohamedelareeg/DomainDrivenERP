using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public Task SendCustomerCreationViaEmailAsync(Task<Customer> customer, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SendInvoiceStatusUpdateToCustomerViaEmailAsync(Customer customer, Invoice invoice, InvoiceStatus invoiceStatus, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task SendInvoiceToCustomerViaEmailAsync(Customer customer, Invoice invoice, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
