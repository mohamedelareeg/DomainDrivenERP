using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Orders.Queries.GetOrdersByCustomerId;
internal class GetOrdersByCustomerIdQueryHandler : IListQueryHandler<GetOrdersByCustomerIdQuery, Order>
{
    private readonly IOrderRepository _orderRepository;
    public async Task<Result<CustomList<Order>>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrdersByCustomerId(request.CustomerId, cancellationToken);
    }
}
