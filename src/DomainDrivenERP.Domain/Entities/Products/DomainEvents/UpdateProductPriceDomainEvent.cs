using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Products.DomainEvents;
public sealed record UpdateProductPriceDomainEvent(Guid ProductId, decimal NewPriceAmount, string NewCurrency) : DomainEvent(Guid.NewGuid());
