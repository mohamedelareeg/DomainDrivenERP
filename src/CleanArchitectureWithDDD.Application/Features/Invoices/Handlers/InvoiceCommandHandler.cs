using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Commands;
using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Handlers
{
    public class InvoiceCommandHandler : ICommandHandler<CreateCustomerInvoiceCommand, bool>, ICommandHandler<UpdateCustomerInvoiceStatusCommand , bool>
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        public InvoiceCommandHandler(ICustomerRespository customerRespository, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _customerRespository = customerRespository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;

        }
        public async Task<Result<bool>> Handle(CreateCustomerInvoiceCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(request.CustomerId);
            if(customer is null)
            {
                return Result.Failure<bool>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Customer with ID {request.CustomerId} not found."));
            }
            var invoice = customer.CreateInvoice(request.InvoiceSerial, request.InvoiceDate, request.InvoiceAmount);
            if (invoice is null)
            {
                return Result.Failure<bool>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Invoice return null Value when trying to Create it."));
            }
            if(invoice.IsSuccess)
            {
                await _customerRespository.AddCustomerInvoiceAsync(customer.Id , invoice.Value);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
             
                return true;
            }
            return false;
        }

        public async Task<Result<bool>> Handle(UpdateCustomerInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(request.CustomerId,cancellationToken);
            if (customer is null)
            {
                return Result.Failure<bool>(new Error("CustomerInvoice.UpdateCustomerInvoiceStatus", $"Customer with ID {request.CustomerId} not found."));
            }

            var invoice = customer.Invoices.FirstOrDefault(a=>a.Id == request.InvoiceId);
            if(invoice is null)
            {
                return Result.Failure<bool>(new Error("CustomerInvoice.UpdateCustomerInvoiceStatus", $"Invoice with ID {request.InvoiceId} not found."));
            }    

            var invoiceUpdated = customer.UpdateCustomerInvoiceStatus(invoice , Domain.Enums.InvoiceStatus.Paid);

            if (invoiceUpdated.IsSuccess)
            {
                await _customerRespository.UpdateInvoiceStatusAsync(invoiceUpdated.Value);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            
                return true;
            }

            return false;
        }
    }
}
