using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;

namespace CleanArchitectureWithDDD.Application.Features.Products.Commands.UpdateProductPrice;
public record UpdateProductPriceCommand(Guid ProductId, decimal NewPriceAmount, string Currency) : ICommand<bool>;
