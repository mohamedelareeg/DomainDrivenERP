using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Queries;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Handlers
{
    public class InvoiceQueryHandler : IQueryHandler<RetriveCustomerInvoiceQuery, bool>
    {
        public Task<Result<bool>> Handle(RetriveCustomerInvoiceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
