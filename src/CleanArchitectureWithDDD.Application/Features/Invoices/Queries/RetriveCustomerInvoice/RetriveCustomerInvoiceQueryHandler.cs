using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Queries.RetriveCustomerInvoice;

internal class RetriveCustomerInvoiceQueryHandler : IQueryHandler<RetriveCustomerInvoiceQuery, bool>
{
    public Task<Result<bool>> Handle(RetriveCustomerInvoiceQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
