using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Orders;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Orders;
internal class CachedOrderRepository : IOrderRepository
{
    private readonly IOrderRepository _decorated;
    private readonly ICacheService _cacheService;

    public CachedOrderRepository(IOrderRepository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _decorated.AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        string key = $"orderById-{orderId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByIdAsync(orderId, cancellationToken),
            cancellationToken);
    }

    public async Task<CustomList<Order>> GetOrdersByCustomerId(Guid customerId, CancellationToken cancellationToken = default)
    {
        string key = $"ordersByCustomerId-{customerId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetOrdersByCustomerId(customerId, cancellationToken),
            cancellationToken);
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _decorated.UpdateAsync(order, cancellationToken);
    }

}
