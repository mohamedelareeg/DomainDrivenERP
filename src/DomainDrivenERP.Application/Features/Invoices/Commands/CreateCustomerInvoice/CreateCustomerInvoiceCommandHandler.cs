﻿using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Invoices.Commands.CreateCustomerInvoice;

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
        Customer? customer = await _customerRespository.GetByIdAsync(request.CustomerId);
        if (customer is null)
        {
            return Result.Failure<Invoice>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Customer with ID {request.CustomerId} not found."));
        }
        bool isInvoiceExist = await _invoiceRepository.IsInvoiceSerialExist(request.InvoiceSerial);
        if (isInvoiceExist)
        {
            return Result.Failure<Invoice>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Invoice Serial  {request.InvoiceSerial} already exist."));
        }
        Result<Invoice>? invoice = customer.CreateInvoice(request.InvoiceSerial, request.InvoiceDate, request.InvoiceAmount);
        if (invoice is null || invoice.IsFailure)
        {
            return Result.Failure<Invoice>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Invoice return null Value when trying to Create it."));
        }

        await _customerRespository.AddCustomerInvoiceAsync(invoice.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return invoice;
    }

}
