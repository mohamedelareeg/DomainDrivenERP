using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Products.Commands.ApplyDiscount;
public record ApplyDiscountCommand(Guid ProductId, decimal DiscountPercentage) : ICommand<decimal>;
