using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;

namespace CleanArchitectureWithDDD.Application.Features.Products.Commands.UpdateProductName;
public record UpdateProductNameCommand(Guid ProductId, string NewName) : ICommand<bool>;

