using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Customers.DomainEvents;

namespace DomainDrivenERP.Application.Features.Customers.Events.CreateCustomer;

public sealed class PerformBackgroundCheckWhenCustomerCreationDomainEventHandler : IDomainEventHandler<CreateCustomerDomainEvent>
{
    public Task Handle(CreateCustomerDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
