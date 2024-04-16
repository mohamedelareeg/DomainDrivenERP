using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Orders;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenERP.Persistence.Repositories.Orders;
internal class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Set<Order>().AddAsync(order, cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Order>().FirstOrDefaultAsync(order => order.Id == orderId, cancellationToken);
    }
    public async Task<CustomList<Order>> GetOrdersByCustomerId(Guid customerId, CancellationToken cancellationToken = default)
    {
        CustomList<Order> orders = await _context.Set<Order>()
            .Where(order => order.CustomerId == customerId)
            .ToCustomListAsync();

        return orders;
    }

    public async Task UpdateAsync(Order order, CancellationToken cancellationToken = default)
    {
        _context.Set<Order>().Update(order);
    }
}
