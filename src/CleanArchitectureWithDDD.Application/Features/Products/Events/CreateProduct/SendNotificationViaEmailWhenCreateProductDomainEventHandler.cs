using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;
using CleanArchitectureWithDDD.Domain.Entities.Products.DomainEvents;

namespace CleanArchitectureWithDDD.Application.Features.Products.Events.CreateProduct;
internal class SendNotificationViaEmailWhenCreateProductDomainEventHandler : IDomainEventHandler<CreateProductDomainEvent>
{
    public Task Handle(CreateProductDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
