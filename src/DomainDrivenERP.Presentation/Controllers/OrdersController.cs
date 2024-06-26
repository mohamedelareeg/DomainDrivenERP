﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Features.Orders.Commands.AddItemtoOrder;
using DomainDrivenERP.Application.Features.Orders.Commands.CancelOrder;
using DomainDrivenERP.Application.Features.Orders.Commands.ChangeOrderStatus;
using DomainDrivenERP.Application.Features.Orders.Commands.CreateOrder;
using DomainDrivenERP.Application.Features.Orders.Commands.RemoveItemfromOrder;
using DomainDrivenERP.Application.Features.Orders.Queries.GetOrderById;
using DomainDrivenERP.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;
[Route("api/v1/orders")]
public class OrdersController : AppControllerBase
{
    public OrdersController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddItemToOrder(AddItemtoOrderCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPut("cancel")]
    public async Task<IActionResult> CancelOrder(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPut("change-status")]
    public async Task<IActionResult> ChangeOrderStatus(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Result<Order> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpDelete("remove-item")]
    public async Task<IActionResult> RemoveItemFromOrder(RemoveItemfromOrderCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> GetOrderById(Guid orderId, CancellationToken cancellationToken)
    {
        Result<Order> result = await Sender.Send(new GetOrderByIdQuery(orderId), cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("by-customer")]
    public async Task<IActionResult> GetOrdersByCustomerId(Guid customerId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        Result<CustomList<Order>> result = await Sender.Send(new GetOrdersByCustomerIdQuery(customerId, fromDate, toDate), cancellationToken);
        return CustomResult(result);
    }
}
