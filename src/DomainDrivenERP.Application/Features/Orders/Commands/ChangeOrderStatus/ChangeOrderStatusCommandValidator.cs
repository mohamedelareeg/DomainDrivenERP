using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Orders.Commands.ChangeOrderStatus;
public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
{
    public ChangeOrderStatusCommandValidator()
    {
        RuleFor(command => command.OrderId).NotEmpty().WithMessage("Order ID must not be empty.");
        RuleFor(command => command.NewStatus).IsInEnum().WithMessage("Invalid order status.");
    }
}
