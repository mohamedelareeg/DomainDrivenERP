using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Queries.RetriveCustomerInvoice
{
    internal class RetriveCustomerInvoiceQueryHandler : IQueryHandler<RetriveCustomerInvoiceQuery, bool>
    {
        public Task<Result<bool>> Handle(RetriveCustomerInvoiceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
