using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Orders.DomainEvents;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Events;
internal class BackgroundServiceWhenRemoveLineItemDomainEventHandler : IDomainEventHandler<RemoveLineItemDomainEvent>
{
    public Task Handle(RemoveLineItemDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
