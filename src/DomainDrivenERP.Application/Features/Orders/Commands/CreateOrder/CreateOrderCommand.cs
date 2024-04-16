using System;
using System.Collections.Generic;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Orders;

namespace DomainDrivenERP.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid CustomerId, List<OrderItemDTO> Items) : ICommand<Order>;

public record OrderItemDTO
{
    public Guid ProductId { get; init; }
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }
}
