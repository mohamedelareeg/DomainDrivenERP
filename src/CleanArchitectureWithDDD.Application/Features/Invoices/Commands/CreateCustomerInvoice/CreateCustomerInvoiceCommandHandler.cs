using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Commands.CreateCustomerInvoice;

internal class CreateCustomerInvoiceCommandHandler : ICommandHandler<CreateCustomerInvoiceCommand, bool>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCustomerInvoiceCommandHandler(ICustomerRespository customerRespository, IUnitOfWork unitOfWork)
    {
        _customerRespository = customerRespository;
        _unitOfWork = unitOfWork;

    }
    public async Task<Result<bool>> Handle(CreateCustomerInvoiceCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Customer? customer = await _customerRespository.GetByIdAsync(request.CustomerId);
        if (customer is null)
        {
            return Result.Failure<bool>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Customer with ID {request.CustomerId} not found."));
        }
        Result<Domain.Entities.Invoice>? invoice = customer.CreateInvoice(request.InvoiceSerial, request.InvoiceDate, request.InvoiceAmount);
        if (invoice is null)
        {
            return Result.Failure<bool>(new Error("CustomerInvoice.CreateCustomerInvoice", $"Invoice return null Value when trying to Create it."));
        }
        if (invoice.IsSuccess)
        {
            await _customerRespository.AddCustomerInvoiceAsync(customer.Id, invoice.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
        return false;
    }

}
