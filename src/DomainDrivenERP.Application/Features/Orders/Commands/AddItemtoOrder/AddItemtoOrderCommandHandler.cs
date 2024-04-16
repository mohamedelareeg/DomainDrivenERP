using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Orders.Commands.AddItemtoOrder;
public class AddItemOrderCommandHandler : ICommandHandler<AddItemtoOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(AddItemtoOrderCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Orders.Order? order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);

        if (order is null)
        {
            return Result.Failure<bool>("Order.AddItemtoOrder", $"Order with ID {request.OrderId} not found.");
        }

        Result<Domain.Entities.Orders.Order> result = order.AddLineItem(request.ProductId, request.ProductPrice, request.Quantity);
        if (result.IsFailure)
        {
            return Result.Failure<bool>(result.Error);
        }

        await _orderRepository.UpdateAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;

    }
}
