using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Products.DomainEvents;
public sealed record UpdateProductPriceDomainEvent(Guid ProductId, decimal NewPriceAmount, string NewCurrency) : DomainEvent(Guid.NewGuid());
