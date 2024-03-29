using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Handlers
{
    public sealed class CustomersDomainEventHandler : INotificationHandler<CreateCustomerDomainEvent>
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IEmailService _emailService;
        public CustomersDomainEventHandler(ICustomerRespository customerRespository , IEmailService emailService)
        {
            _customerRespository = customerRespository;
            _emailService = emailService;
        }
        public async Task Handle(CreateCustomerDomainEvent notification, CancellationToken cancellationToken)
        {
            var customer = _customerRespository.GetByIdAsync(notification.CustomerId,cancellationToken);
            if(customer is null)
            {
                return;
            }
            await _emailService.SendCustomerCreationViaEmailAsync(customer,cancellationToken);

        }
    }
}
