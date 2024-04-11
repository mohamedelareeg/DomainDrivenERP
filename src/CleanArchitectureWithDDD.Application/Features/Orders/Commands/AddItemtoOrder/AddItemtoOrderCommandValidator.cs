using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Commands.AddItemtoOrder;
public class AddItemtoOrderCommandValidator : AbstractValidator<AddItemtoOrderCommand>
{
    public AddItemtoOrderCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty().WithMessage("Order ID must not be empty.");
        RuleFor(command => command.ProductId).NotEmpty().WithMessage("Product ID must not be empty.");
        RuleFor(command => command.ProductPrice).GreaterThan(0).WithMessage("Product price must be greater than zero.");
        RuleFor(command => command.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
    }
}
