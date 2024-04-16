using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Orders.Commands.CreateOrder;
internal class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Order>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRespository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRespository customerRespository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRespository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Customers.Customer? customer = await _customerRepository.GetByIdAsync(request.CustomerId.ToString());
        if (customer == null)
        {
            return Result.Failure<Order>("Order.CreateOrder", $"Customer with ID {request.CustomerId} not found.");
        }

        Result<Order> createOrderResult = Order.Create(request.CustomerId);
        if (createOrderResult.IsFailure)
        {
            return Result.Failure<Order>(createOrderResult.Error);
        }
        Order order = createOrderResult.Value;
        foreach (OrderItemDTO itemDto in request.Items)
        {
            Domain.Entities.Products.Product product = await _productRepository.GetByIdAsync(itemDto.ProductId);
            if (product == null)
            {
                return Result.Failure<Order>("Order.CreateOrder", $"Product with ID {itemDto.ProductId} not found.");
            }

            Result<Order> addLineItemResult = order.AddLineItem(product.Id, product.Price.Amount, itemDto.Quantity);
            if (addLineItemResult.IsFailure)
            {
                return Result.Failure<Order>(addLineItemResult.Error);
            }
        }

        await _orderRepository.AddAsync(order, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return order;
    }
}
