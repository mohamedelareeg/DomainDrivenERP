using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Customers;

internal class CachedCustomerRepository : ICustomerRespository
{
    private readonly ICustomerRespository _decorated;
    private readonly ICacheService _cacheService;

    public CachedCustomerRepository(ICustomerRespository decorated, ICacheService cacheService)
    {
        _decorated = decorated;
        _cacheService = cacheService;
    }

    public Task AddAsync(Customer customer)
    {
        return _decorated.AddAsync(customer);
    }

    public Task AddCustomerInvoiceAsync(Invoice invoice)
    {
        return _decorated.AddCustomerInvoiceAsync(invoice);
    }

    public async Task<CustomList<Customer>?> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        string key = "all-customers";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetAllCustomers(cancellationToken),
            cancellationToken);
    }

    public async Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default)
    {
        string key = $"customer-{CustomerId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetByIdAsync(CustomerId, cancellationToken),
            cancellationToken);
    }

    public async Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default)
    {
        string key = $"customer-invoices-{customerId}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.GetCustomerInvoicesById(customerId, cancellationToken),
            cancellationToken);
    }


    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
        string key = $"email-unique-{value}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.IsEmailUniqueAsync(value, cancellationToken),
            cancellationToken);
    }


    public Task Update(Customer customer)
    {
        return _decorated.Update(customer);
    }

    public Task UpdateInvoiceStatus(Invoice invoiceUpdated)
    {
        return _decorated.UpdateInvoiceStatus(invoiceUpdated);
    }
}
