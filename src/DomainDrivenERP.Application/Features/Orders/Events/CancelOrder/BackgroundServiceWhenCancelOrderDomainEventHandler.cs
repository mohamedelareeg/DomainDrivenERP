using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders.DomainEvents;

namespace DomainDrivenERP.Application.Features.Orders.Events.CancelOrder;
internal class BackgroundServiceWhenCancelOrderDomainEventHandler : IDomainEventHandler<CancelOrderDomainEvent>
{
    public Task Handle(CancelOrderDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
