using DomainDrivenERP.Domain.Abstractions.Infrastructure.Services;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Customers.DomainEvents;
using MediatR;

namespace DomainDrivenERP.Application.Features.Invoices.Events;

internal class SendInvoiceStatusUpdateToCustomerViaEmailDomainEventHandler : INotificationHandler<UpdateInvoiceStatusDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly ICustomerRespository _customerRespository;
    public SendInvoiceStatusUpdateToCustomerViaEmailDomainEventHandler(IEmailService emailService, ICustomerRespository customerRespository)
    {
        _emailService = emailService;
        _customerRespository = customerRespository;
    }

    public async Task Handle(UpdateInvoiceStatusDomainEvent notification, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRespository.GetByIdAsync(notification.CustomerId.ToString(), cancellationToken);
        if (customer is null)
        {
            return;
        }
        await _emailService.SendInvoiceStatusUpdateToCustomerViaEmailAsync(customer, notification.Invoice, notification.InvoiceStatus);
    }
}
