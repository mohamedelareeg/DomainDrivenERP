using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders.DomainEvents;

namespace DomainDrivenERP.Application.Features.Orders.Events.CreateOrder;
internal class BackgroundServiceWhenCreateOrderDomainEventHandler : IDomainEventHandler<CreateOrderDomainEvent>
{
    public Task Handle(CreateOrderDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
