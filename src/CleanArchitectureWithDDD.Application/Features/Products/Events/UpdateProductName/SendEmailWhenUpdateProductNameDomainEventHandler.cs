using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Products.DomainEvents;

namespace CleanArchitectureWithDDD.Application.Features.Products.Events.UpdateProductName;
internal class SendEmailWhenUpdateProductNameDomainEventHandler : IDomainEventHandler<UpdateProductNameDomainEvent>
{
    public Task Handle(UpdateProductNameDomainEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
