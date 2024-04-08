using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Cached;
internal class CachedInvoiceRepository : IInvoiceRepository
{
    public readonly IInvoiceRepository _decorated;
    public readonly IMemoryCache _memoryCache;//Memory Cache
    public readonly IDistributedCache _distributedCache; //Redis Caching
    public readonly ApplicationDbContext _context;

    public CachedInvoiceRepository(IInvoiceRepository decorated, IMemoryCache memoryCache, IDistributedCache distributedCache, ApplicationDbContext context)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _context = context;
    }

    public async Task<CustomList<Invoice>> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        string key = $"customerInvoices-{customerId}-Page{pageNumber}-Size{pageSize}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            return await _decorated.GetAllCustomerInvoices(customerId, startDate, endDate, pageSize,pageNumber);
        });
    }

    public async Task<CustomList<Invoice>> GetAllCustomerInvoicesWithDapper(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        string key = $"customerInvoices-{customerId}-Page{pageNumber}-Size{pageSize}";
        //Get Value for the key for redis cache
        string? cachedInvoices = await _distributedCache.GetStringAsync(key, cancellationToken);
        CustomList<Invoice> invoices;
        //See if value exist
        if (string.IsNullOrEmpty(cachedInvoices))
        {
            invoices = await _decorated.GetAllCustomerInvoicesWithDapper(customerId, startDate, endDate, pageSize, pageNumber);
            // check if nulll
            if (invoices == null)
            {
                return invoices;
            }
            //if not null caching with redis
            await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(invoices), cancellationToken);
            //return the value
            return invoices;
        }
        //if value exist
        invoices = JsonConvert.DeserializeObject<CustomList<Invoice>>(
            cachedInvoices,
            new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor, // it will look for the non constractor in the invoices
                ContractResolver = new PrivateResolver()
            });
        //EF Change Traking
        if (invoices.Items.Count > 0)
        {
            _context.Set<Invoice>().AttachRange(invoices.Items);
        }
        //return the value
        return invoices;
    }

    public Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default)
    {
        return _decorated.IsInvoiceSerialExist(invoiceSerial);
    }
}
