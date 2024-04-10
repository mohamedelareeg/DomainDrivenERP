using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Caching;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Persistence.Data;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Invoices;
internal class CachedInvoiceRepository : IInvoiceRepository
{
    private readonly IInvoiceRepository _decorated;
    private readonly ICacheService _cacheService;
    private readonly ApplicationDbContext _context;

    public CachedInvoiceRepository(IInvoiceRepository decorated, ICacheService cacheService, ApplicationDbContext context)
    {
        _decorated = decorated;
        _cacheService = cacheService;
        _context = context;
    }

    public async Task<CustomList<Invoice>?> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        string key = $"customerInvoices-{customerId}-Page{pageNumber}-Size{pageSize}";
        return await _cacheService.GetOrSetAsync(
            key,
            async () =>
            {
                CustomList<Invoice>? invoices = await _decorated.GetAllCustomerInvoices(customerId, startDate, endDate, pageSize, pageNumber, cancellationToken);
                if (invoices is not null && invoices.Items.Count > 0)
                {
                    _context.Set<Invoice>().AttachRange(invoices.Items);
                }
                return invoices;
            },
            cancellationToken);
    }
    public async Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default)
    {
        string key = $"InvoiceSerialExist-{invoiceSerial}";
        return await _cacheService.GetOrSetAsync(key,
            async () => await _decorated.IsInvoiceSerialExist(invoiceSerial, cancellationToken),
            cancellationToken);
    }
}
