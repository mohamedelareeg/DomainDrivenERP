using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.DomainEvents;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Events;

public sealed class PerformBackgroundCheckWhenCustomerCreationDomainEventHandler : IDomainEventHandler<CreateCustomerDomainEvent>
{
    public Task Handle(CreateCustomerDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
