using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Events;

internal class SendInvoiceToCustomerViaEmailDomainEventHandler : INotificationHandler<CreateInvoiceDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ICustomerRespository _customerRespository;
    public SendInvoiceToCustomerViaEmailDomainEventHandler(IEmailService emailService, ICustomerRespository customerRespository)
    {
        _emailService = emailService;
        _customerRespository = customerRespository;
    }
    public async Task Handle(CreateInvoiceDomainEvent notification, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRespository.GetByIdAsync(notification.CustomerId.ToString(), cancellationToken);
        if (customer is null)
        {
            return;
        }
        await _emailService.SendInvoiceToCustomerViaEmailAsync(customer, notification.Invoice);
    }
}
