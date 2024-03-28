using CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Handlers
{
    public class InvoiceQueryHandler : IRequestHandler<RetriveCustomerInvoiceQuery, bool>
    {
        public Task<bool> Handle(RetriveCustomerInvoiceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
