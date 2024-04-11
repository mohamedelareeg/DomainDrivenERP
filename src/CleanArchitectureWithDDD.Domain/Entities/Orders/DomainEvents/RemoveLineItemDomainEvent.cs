using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Orders.DomainEvents;
public sealed record RemoveLineItemDomainEvent(Guid OrderId, Guid LineItemId) : DomainEvent(Guid.NewGuid());

