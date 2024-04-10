using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Commands.UpdateCustomerInvoice;
internal class UpdateCustomerInvoiceCommandHandler : ICommandHandler<UpdateCustomerInvoiceCommand, Invoice>
{
    public Task<Result<Invoice>> Handle(UpdateCustomerInvoiceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
