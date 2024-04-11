using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Commands.CreateOrder;
public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");

        RuleFor(x => x.Items)
            .NotNull().WithMessage("Items list cannot be null.")
            .NotEmpty().WithMessage("Items list cannot be empty.");

        RuleForEach(x => x.Items)
            .SetValidator(new OrderItemValidator());
    }
    private sealed class OrderItemValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product ID is required.");

            RuleFor(x => x.ProductPrice).GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        }
    }
}
