using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders.DomainEvents;

namespace DomainDrivenERP.Application.Features.Orders.Events.ChangeOrderStatus;
internal class BackgroundServiceWhenChangeOrderStatusDomainEventHandler : IDomainEventHandler<ChangeOrderStatusDomainEvent>
{
    public Task Handle(ChangeOrderStatusDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
