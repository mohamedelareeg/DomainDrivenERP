using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using CleanArchitectureWithDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitectureWithDDD.Domain.Errors.DomainErrors;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Handlers
{
    public sealed class InvoiceDomainEventHandler : INotificationHandler<CreateInvoiceDomainEvent>,INotificationHandler<UpdateInvoiceStatusDomainEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ICustomerRespository _customerRespository;
        public InvoiceDomainEventHandler(IEmailService emailService, ICustomerRespository customerRespository)
        {
            _emailService = emailService;
            _customerRespository = customerRespository;
        }
        public async Task Handle(CreateInvoiceDomainEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(notification.CustomerId , cancellationToken);
            if (customer is null)
            {
                return;
            }
            await _emailService.SendInvoiceToCustomerViaEmailAsync(customer, notification.Invoice);
        }

        public async Task Handle(UpdateInvoiceStatusDomainEvent notification, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(notification.CustomerId, cancellationToken);
            if (customer is null)
            {
                return;
            }
            await _emailService.SendInvoiceStatusUpdateToCustomerViaEmailAsync(customer, notification.Invoice , notification.InvoiceStatus);
        }
    }
}
