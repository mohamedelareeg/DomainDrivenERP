using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Events
{
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
            var customer = await _customerRespository.GetByIdAsync(notification.CustomerId, cancellationToken);
            if (customer is null)
            {
                return;
            }
            await _emailService.SendInvoiceStatusUpdateToCustomerViaEmailAsync(customer, notification.Invoice, notification.InvoiceStatus);
        }
    }
}
