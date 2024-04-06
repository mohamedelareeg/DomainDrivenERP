﻿using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Events;

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
        Domain.Entities.Customer? customer = await _customerRespository.GetByIdAsync(notification.CustomerId.ToString(), cancellationToken);
        if (customer is null)
        {
            return;
        }
        await _emailService.SendInvoiceStatusUpdateToCustomerViaEmailAsync(customer, notification.Invoice, notification.InvoiceStatus);
    }
}
