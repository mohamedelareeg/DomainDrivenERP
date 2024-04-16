using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Products.DomainEvents;

namespace DomainDrivenERP.Application.Features.Products.Events.CreateProduct;
internal class PerformBackGroundServiceWhenCreateProductDomainEventHandler : IDomainEventHandler<CreateProductDomainEvent>
{
    public Task Handle(CreateProductDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
