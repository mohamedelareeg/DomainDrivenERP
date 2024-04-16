using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders.DomainEvents;

namespace DomainDrivenERP.Application.Features.Orders.Events;
internal class BackgroundServiceWhenRemoveLineItemDomainEventHandler : IDomainEventHandler<RemoveLineItemDomainEvent>
{
    public Task Handle(RemoveLineItemDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
