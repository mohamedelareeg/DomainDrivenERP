using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Orders.DomainEvents;
public sealed record RemoveLineItemDomainEvent(Guid OrderId, Guid LineItemId) : DomainEvent(Guid.NewGuid());

