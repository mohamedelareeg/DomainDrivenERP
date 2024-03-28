using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Commands
{
    public class UpdateCustomerInvoiceStatusCommand : IRequest<Result<bool>>
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
