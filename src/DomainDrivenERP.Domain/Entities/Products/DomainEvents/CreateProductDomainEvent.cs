using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Products.DomainEvents;
public sealed record CreateProductDomainEvent(Guid ProductId, string Name, decimal PriceAmount, string Currency, int StockQuantity, string SKU, string Model, string Details, Guid CategoryId) : DomainEvent(Guid.NewGuid());

