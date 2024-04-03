using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Cached;
internal class CachedCustomerRepository : ICustomerRespository
{
    private readonly ICustomerRespository _decorated;
    // First way require CustomerRespository  and other way with Scrutor Requrie the Interface ICustomerRespository
    private readonly IMemoryCache _memoryCache;

    public CachedCustomerRepository(ICustomerRespository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public Task AddAsync(Customer customer)
    {
        return _decorated.AddAsync(customer);
    }

    public Task AddCustomerInvoiceAsync(Guid id, Invoice invoice)
    {
       return _decorated.AddCustomerInvoiceAsync(id, invoice);
    }

    public Task<List<Customer>> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        return _decorated.GetAllCustomers(cancellationToken);
    }

    public Task<Customer?> GetByIdAsync(Guid CustomerId, CancellationToken cancellationToken = default)
    {
        string key = $"customer-{CustomerId}";
        return _memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            return _decorated.GetByIdAsync(CustomerId, cancellationToken);
        });
        
    }

    public Task<dynamic?> GetByIdAsync_Dapper(Guid customerId)
    {
       return _decorated.GetByIdAsync_Dapper(customerId);
    }

    public Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
       return _decorated.IsEmailUniqueAsync(value, cancellationToken);
    }

    public Task UpdateAsync(Customer customer)
    {
        return _decorated.UpdateAsync(customer);
    }

    public Task UpdateInvoiceStatusAsync(Invoice invoiceUpdated)
    {
        return _decorated.UpdateInvoiceStatusAsync(invoiceUpdated);
    }
}
