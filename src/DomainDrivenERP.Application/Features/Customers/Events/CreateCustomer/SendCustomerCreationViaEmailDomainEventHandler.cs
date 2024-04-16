using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Infrastructure.Services;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Customers.DomainEvents;

namespace DomainDrivenERP.Application.Features.Customers.Events.CreateCustomer;

public sealed class SendCustomerCreationViaEmailDomainEventHandler : IDomainEventHandler<CreateCustomerDomainEvent>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly IEmailService _emailService;
    public SendCustomerCreationViaEmailDomainEventHandler(ICustomerRespository customerRespository, IEmailService emailService)
    {
        _customerRespository = customerRespository;
        _emailService = emailService;
    }
    public async Task Handle(CreateCustomerDomainEvent notification, CancellationToken cancellationToken)
    {
        Task<Customer?>? customer = _customerRespository.GetByIdAsync(notification.CustomerId.ToString(), cancellationToken);
        if (customer is null)
        {
            return;
        }
        await _emailService.SendCustomerCreationViaEmailAsync(customer, cancellationToken);

    }
}
