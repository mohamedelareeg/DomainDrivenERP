using CleanArchitectureWithDDD.Application.Features.Invoices.Requests.Commands;
using CleanArchitectureWithDDD.Domain.Abstractions;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Handlers
{
    public class InvoiceCommandHandler : IRequestHandler<CreateCustomerInvoiceCommand, bool>, IRequestHandler<UpdateCustomerInvoiceStatusCommand , Result<bool>>
    {
        private readonly ICustomerRespository _customerRespository;
        public InvoiceCommandHandler(ICustomerRespository customerRespository)
        {
            _customerRespository = customerRespository;
        }
        public Task<bool> Handle(CreateCustomerInvoiceCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> Handle(UpdateCustomerInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(request.CustomerId);
            if (customer is null)
            {
                return Result.Failure<bool>(new Error("CustomerInvocies.UpdateCustomerInvoiceStatus", $"Customer with ID {request.CustomerId} not found."));
            }

            var invoice = customer.Invoices.FirstOrDefault(a=>a.Id == request.InvoiceId);
            if(invoice is null)
            {
                return Result.Failure<bool>(new Error("CustomerInvocies.UpdateCustomerInvoiceStatus", $"Invoice with ID {request.InvoiceId} not found."));
            }    

            var invoiceUpdated = customer.UpdateCustomerInvoiceStatus(invoice , Domain.Enums.InvoiceStatus.Paid);

            if (invoiceUpdated.IsSuccess)
            {
                await _customerRespository.UpdateAsync(customer);
                return true;
            }

            return false;
        }
    }
}
