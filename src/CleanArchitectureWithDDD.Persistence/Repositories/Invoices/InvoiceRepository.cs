using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Invoices;
internal class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _context;
    public InvoiceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CustomList<Invoice>?> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        IQueryable<Invoice> query = _context.Set<Invoice>().Where(i => i.CustomerId.ToString() == customerId);

        if (startDate.HasValue)
        {
            query = query.Where(i => i.InvoiceDate >= startDate);
        }

        if (endDate.HasValue)
        {
            query = query.Where(i => i.InvoiceDate <= endDate);
        }

        // Calculate total count
        int totalCount = await query.CountAsync();

        // Calculate total pages
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        // Add pagination
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        List<Invoice> result = await query.ToListAsync();

        return result.ToCustomList(totalCount, totalPages);
    }


    public async Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Invoice>()
            //.IgnoreQueryFilters() // Ignore any query filters applied (e.g., cancelled invoices)
            //.Where(a => a.Cancelled) // If i do the opposite in the Query Filtering it will apply the both equal and not equal (if i don't add query filter and apply query filter in the configration)
            .AnyAsync(a => a.InvoiceSerial == invoiceSerial);
    }
}
