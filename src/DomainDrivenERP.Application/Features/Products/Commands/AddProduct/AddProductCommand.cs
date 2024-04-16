using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Products;

namespace DomainDrivenERP.Application.Features.Products.Commands.AddProduct;
public record AddProductCommand(Guid CategoryId, Guid ProductId, string ProductName, decimal Amount, string Currency, int StockQuantity, string SKU, string Model, string Details) : ICommand<Product>;
