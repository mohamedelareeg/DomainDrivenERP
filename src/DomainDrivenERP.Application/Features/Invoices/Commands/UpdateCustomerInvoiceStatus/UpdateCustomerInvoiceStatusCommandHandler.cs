using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Invoices.Commands.UpdateCustomerInvoiceStatus;

internal class UpdateCustomerInvoiceStatusCommandHandler : ICommandHandler<UpdateCustomerInvoiceStatusCommand, bool>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCustomerInvoiceStatusCommandHandler(ICustomerRespository customerRespository, IUnitOfWork unitOfWork)
    {
        _customerRespository = customerRespository;
        _unitOfWork = unitOfWork;

    }

    public async Task<Result<bool>> Handle(UpdateCustomerInvoiceStatusCommand request, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRespository.GetByIdAsync(request.CustomerId.ToString(), cancellationToken);
        if (customer is null)
        {
            return Result.Failure<bool>(new Error("CustomerInvoice.UpdateCustomerInvoiceStatus", $"Customer with ID {request.CustomerId} not found."));
        }

        Invoice? invoice = customer.Invoices.FirstOrDefault(a => a.Id == request.InvoiceId);
        if (invoice is null)
        {
            return Result.Failure<bool>(new Error("CustomerInvoice.UpdateCustomerInvoiceStatus", $"Invoice with ID {request.InvoiceId} not found."));
        }

        Result<Invoice> invoiceUpdated = customer.UpdateCustomerInvoiceStatus(invoice, Domain.Enums.InvoiceStatus.Paid);

        if (invoiceUpdated.IsSuccess)
        {
            await _customerRespository.UpdateInvoiceStatus(invoiceUpdated.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

        return false;
    }
}
