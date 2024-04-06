using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Commands.CreateCustomerInvoice;

internal class CreateCustomerInvoiceCommandHandler : ICommandHandler<CreateCustomerInvoiceCommand, Invoice>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvoiceRepository _invoiceRepository;
    public CreateCustomerInvoiceCommandHandler(ICustomerRespository customerRespository, IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository)
    {
        _customerRespository = customerRespository;
        _unitOfWork = unitOfWork;
        _invoiceRepository = invoiceRepository;
    }
    public async Task<Result<Invoice>> Handle(CreateCustomerInvoiceCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Customer? customer = await _customerRespository.GetByIdAsync(request.CustomerId);
        if (customer is null)
        {
            return Result.Failure<Invoice>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Customer with ID {request.CustomerId} not found."));
        }
        bool isInvoiceExist = await _invoiceRepository.IsInvoiceSerialExist(request.InvoiceSerial);
        if (isInvoiceExist)
        {
            return Result.Failure<Invoice>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Invoice Serial  {request.InvoiceSerial} already exist."));
        }
        Result<Domain.Entities.Invoice>? invoice = customer.CreateInvoice(request.InvoiceSerial, request.InvoiceDate, request.InvoiceAmount);
        if (invoice is null || invoice.IsFailure)
        {
            return Result.Failure<Invoice>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Invoice return null Value when trying to Create it."));
        }

        await _customerRespository.AddCustomerInvoiceAsync(invoice.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invoice;
    }

}
