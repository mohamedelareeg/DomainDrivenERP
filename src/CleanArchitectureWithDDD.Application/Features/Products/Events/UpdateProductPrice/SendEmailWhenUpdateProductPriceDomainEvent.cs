using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Products.DomainEvents;

namespace CleanArchitectureWithDDD.Application.Features.Products.Events.UpdateProductPrice;
internal class SendEmailWhenUpdateProductPriceDomainEvent : IDomainEventHandler<UpdateProductPriceDomainEvent>
{
    public Task Handle(UpdateProductPriceDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
