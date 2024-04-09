using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Invoices;
internal class CachedInvoiceRepository : IInvoiceRepository
{
    public readonly IInvoiceRepository _decorated;
    public readonly IMemoryCache _memoryCache;//Memory Cache
    public readonly IDistributedCache _distributedCache; //Redis Caching
    public readonly ApplicationDbContext _context;
    private readonly DistributedCacheEntryOptions _cacheOptions;

    public CachedInvoiceRepository(IInvoiceRepository decorated, IMemoryCache memoryCache, IDistributedCache distributedCache, ApplicationDbContext context)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _context = context;

        // Set global cache options
        _cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        };
    }

    public async Task<CustomList<Invoice>> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        string key = $"customerInvoices-{customerId}-Page{pageNumber}-Size{pageSize}";
        string? cachedInvoices = await _distributedCache.GetStringAsync(key, cancellationToken);
        CustomList<Invoice> invoices;
        if (string.IsNullOrEmpty(cachedInvoices))
        {
            invoices = await _decorated.GetAllCustomerInvoices(customerId, startDate, endDate, pageSize, pageNumber);
            if (invoices == null)
            {
                return invoices;
            }
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(invoices), _cacheOptions, cancellationToken);
            return invoices;
        }
        invoices = JsonConvert.DeserializeObject<CustomList<Invoice>>(
            cachedInvoices,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor, // it will look for the non constractor in the invoices
                ContractResolver = new PrivateResolver(),
                Converters = { new ValueObjectJsonConverter() }
            });
        //EF Change Traking
        if (invoices.Items.Count > 0)
        {
            _context.Set<Invoice>().AttachRange(invoices.Items);
        }
        return invoices;
    }
    public async Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default)
    {
        string key = $"InvoiceSerialExist-{invoiceSerial}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            return await _decorated.IsInvoiceSerialExist(invoiceSerial);
        });
    }
}
