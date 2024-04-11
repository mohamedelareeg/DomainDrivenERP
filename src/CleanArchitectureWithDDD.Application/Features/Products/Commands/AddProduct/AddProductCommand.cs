using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Products;

namespace CleanArchitectureWithDDD.Application.Features.Products.Commands.AddProduct;
public record AddProductCommand(Guid CategoryId, Guid ProductId, string ProductName, decimal Amount, string Currency, int StockQuantity, string SKU, string Model, string Details) : ICommand<Product>;
