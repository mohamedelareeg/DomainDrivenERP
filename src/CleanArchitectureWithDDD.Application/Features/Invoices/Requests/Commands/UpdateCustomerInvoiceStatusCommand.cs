using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Commands
{
    public class UpdateCustomerInvoiceStatusCommand : ICommand<bool>
    {
        public UpdateCustomerInvoiceStatusCommand(Guid customerId, Guid invoiceId)
        {
            CustomerId = customerId;
            InvoiceId = invoiceId;
        }

        public Guid CustomerId { get; }
        public Guid InvoiceId { get; }
    }
}
