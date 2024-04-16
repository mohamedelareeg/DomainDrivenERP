using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Invoices.Commands.UpdateCustomerInvoice;
internal class UpdateCustomerInvoiceCommandHandler : ICommandHandler<UpdateCustomerInvoiceCommand, Invoice>
{
    public Task<Result<Invoice>> Handle(UpdateCustomerInvoiceCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
