using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Orders.Commands.RemoveItemfromOrder;
public class RemoveItemfromOrderCommandValidator : AbstractValidator<RemoveItemfromOrderCommand>
{
    public RemoveItemfromOrderCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty().WithMessage("Order ID must not be empty.");
        RuleFor(command => command.LineItemId).NotEmpty().WithMessage("Line item ID must not be empty.");
    }
}
