using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Cached;
internal class CachedInvoiceRepository : IInvoiceRepository
{
    public readonly IInvoiceRepository _decorated;
    public readonly IMemoryCache _memoryCache;

    public CachedInvoiceRepository(IInvoiceRepository decorated, IMemoryCache memoryCache)
    {
        _decorated = decorated;
        _memoryCache = memoryCache;
    }

    public async Task<CustomList<Invoice>> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber)
    {
        string key = $"customerInvoices-{customerId}-Page{pageNumber}-Size{pageSize}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            return await _decorated.GetAllCustomerInvoices(customerId, startDate, endDate, pageSize,pageNumber);
        });
    }

    public async Task<CustomList<Invoice>> GetAllCustomerInvoicesWithDapper(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber)
    {
        string key = $"customerInvoices-{customerId}-Page{pageNumber}-Size{pageSize}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
            return await _decorated.GetAllCustomerInvoicesWithDapper(customerId, startDate, endDate, pageSize, pageNumber);
        });
    }

    public Task<bool> IsInvoiceSerialExist(string invoiceSerial)
    {
        return _decorated.IsInvoiceSerialExist(invoiceSerial);
    }
}
