using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Customers;

internal class CachedCustomerRepository : ICustomerRespository
{
    private readonly ICustomerRespository _decorated;
    private readonly IDistributedCache _distributedCache;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CachedCustomerRepository(ICustomerRespository decorated, IDistributedCache distributedCache)
    {
        _decorated = decorated;
        _distributedCache = distributedCache;

        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }
    public Task AddAsync(Customer customer)
    {
        return _decorated.AddAsync(customer);
    }

    public Task AddCustomerInvoiceAsync(Invoice invoice)
    {
        return _decorated.AddCustomerInvoiceAsync(invoice);
    }

    public async Task<CustomList<Customer>> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        string key = "all-customers";

        string? cachedCustomers = await _distributedCache.GetStringAsync(key, cancellationToken);

        CustomList<Customer> customers;
        if (string.IsNullOrEmpty(cachedCustomers))
        {
            customers = await _decorated.GetAllCustomers(cancellationToken);
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(customers), _cacheOptions, cancellationToken);

            return customers;
        }

        return JsonConvert.DeserializeObject<CustomList<Customer>>(cachedCustomers, new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateResolver(),
            Converters = { new ValueObjectJsonConverter() }
        });
    }

    public async Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default)
    {
        string key = $"customer-{CustomerId}";

        string? cachedCustomer = await _distributedCache.GetStringAsync(key, cancellationToken);

        Customer? customer;
        if (string.IsNullOrEmpty(cachedCustomer))
        {
            customer = await _decorated.GetByIdAsync(CustomerId, cancellationToken);
            if (customer == null)
                return null;

            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(customer), _cacheOptions, cancellationToken);
            return customer;
        }

        return JsonConvert.DeserializeObject<Customer>(cachedCustomer, new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateResolver(),
            Converters = { new ValueObjectJsonConverter() }
        });
    }

    public async Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default)
    {
        string key = $"customer-invoices-{customerId}";

        string? cachedCustomerInvoices = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedCustomerInvoices))
        {
            Customer? customer = await _decorated.GetCustomerInvoicesById(customerId, cancellationToken);
            if (customer == null)
                return null;

            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(customer), _cacheOptions, cancellationToken);
            return customer;
        }

        return JsonConvert.DeserializeObject<Customer>(cachedCustomerInvoices, new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            ContractResolver = new PrivateResolver(),
            Converters = { new ValueObjectJsonConverter() }
        });
    }


    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
        string key = $"email-unique-{value}";

        string? cachedResult = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrEmpty(cachedResult))
        {
            bool isUnique = await _decorated.IsEmailUniqueAsync(value, cancellationToken);
            await _distributedCache.SetStringAsync(key, isUnique.ToString(), _cacheOptions, cancellationToken);
            return isUnique;
        }

        return bool.Parse(cachedResult);
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
