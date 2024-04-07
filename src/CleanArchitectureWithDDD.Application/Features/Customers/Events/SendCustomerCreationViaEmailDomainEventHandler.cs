using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Infrastructure.Services;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using CleanArchitectureWithDDD.Domain.Entities.Customers;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Events;

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
