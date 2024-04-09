using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Queries.RetriveCustomerInvoice;

internal class RetriveCustomerInvoicesQueryHandler : IListQueryHandler<RetriveCustomerInvoicesQuery, Invoice>
{
    private readonly IInvoiceRepository _invoiceRepository;
    public RetriveCustomerInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    public async Task<Result<CustomList<Invoice>>> Handle(RetriveCustomerInvoicesQuery request, CancellationToken cancellationToken)
    {
        CustomList<Invoice> invoices = await _invoiceRepository.GetAllCustomerInvoices(request.CustomerId, request.StartDate, request.EndDate, request.PageSize, request.PageNumber);
        return invoices;
    }

}
