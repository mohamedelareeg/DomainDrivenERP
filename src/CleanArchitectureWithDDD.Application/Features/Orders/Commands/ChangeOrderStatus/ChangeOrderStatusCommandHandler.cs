﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Data;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Commands.ChangeOrderStatus;
public class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeOrderStatusCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ChangeOrderStatusCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Orders.Order order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure<bool>("Order.ChangeOrderStatus", $"Order with ID {request.OrderId} not found.");
        }
        Result<Domain.Entities.Orders.Order> result = order.ChangeStatus(request.NewStatus);
        if (result.IsFailure)
        {
            return Result.Failure<bool>(result.Error);
        }

        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
