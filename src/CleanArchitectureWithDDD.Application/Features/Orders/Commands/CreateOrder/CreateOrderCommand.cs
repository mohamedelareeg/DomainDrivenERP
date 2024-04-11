using System;
using System.Collections.Generic;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Orders;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(Guid CustomerId, List<OrderItemDTO> Items) : ICommand<Order>;

public record OrderItemDTO
{
    public Guid ProductId { get; init; }
    public decimal ProductPrice { get; init; }
    public int Quantity { get; init; }
}
