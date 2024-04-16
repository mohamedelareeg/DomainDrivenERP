using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Products.DomainEvents;

namespace DomainDrivenERP.Application.Features.Products.Events.UpdateProductName;
internal class SendEmailWhenUpdateProductNameDomainEventHandler : IDomainEventHandler<UpdateProductNameDomainEvent>
{
    public Task Handle(UpdateProductNameDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
