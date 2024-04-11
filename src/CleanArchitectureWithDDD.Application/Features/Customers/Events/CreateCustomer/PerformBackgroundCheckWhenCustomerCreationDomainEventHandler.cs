using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Events.CreateCustomer;

public sealed class PerformBackgroundCheckWhenCustomerCreationDomainEventHandler : IDomainEventHandler<CreateCustomerDomainEvent>
{
    public Task Handle(CreateCustomerDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
