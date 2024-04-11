using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Products.DomainEvents;
public sealed record CreateProductDomainEvent(Guid ProductId, string Name, decimal PriceAmount, string Currency, int StockQuantity, string SKU, string Model, string Details, Guid CategoryId) : DomainEvent(Guid.NewGuid());

