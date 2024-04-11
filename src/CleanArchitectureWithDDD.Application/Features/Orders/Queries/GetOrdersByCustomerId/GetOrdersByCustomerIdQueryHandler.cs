using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Orders;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Orders.Queries.GetOrdersByCustomerId;
internal class GetOrdersByCustomerIdQueryHandler : IListQueryHandler<GetOrdersByCustomerIdQuery, Order>
{
    private readonly IOrderRepository _orderRepository;
    public async Task<Result<CustomList<Order>>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrdersByCustomerId(request.CustomerId, cancellationToken);
    }
}
