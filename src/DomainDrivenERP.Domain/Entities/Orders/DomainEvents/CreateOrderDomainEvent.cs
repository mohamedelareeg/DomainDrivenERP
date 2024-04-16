using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Orders.DomainEvents;
public sealed record CreateOrderDomainEvent(Guid OrderId, Guid CustomerId) : DomainEvent(Guid.NewGuid());
