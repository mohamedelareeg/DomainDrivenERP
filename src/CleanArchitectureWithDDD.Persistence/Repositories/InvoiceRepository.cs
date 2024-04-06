using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Repositories;
internal class InvoiceRepository : IInvoiceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly ISqlConnectionFactory _connectionFactory;

    public InvoiceRepository(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
    {
        _context = context;
        _connectionFactory = connectionFactory;
    }
    public async Task<CustomList<Invoice>> GetAllCustomerInvoicesWithDapper(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber)
    {
        await using SqlConnection sqlConnection = _connectionFactory.SqlConnection();

        string countQuery = "SELECT COUNT(*) FROM Invoices WHERE CustomerId = @CustomerId";
        string query = @"SELECT * FROM Invoices WHERE CustomerId = @CustomerId";

        if (startDate.HasValue && endDate.HasValue)
        {
            countQuery += " AND InvoiceDate >= @StartDate AND InvoiceDate <= @EndDate";
            query += " AND InvoiceDate >= @StartDate AND InvoiceDate <= @EndDate";
        }
        else if (startDate.HasValue)
        {
            countQuery += " AND InvoiceDate >= @StartDate";
            query += " AND InvoiceDate >= @StartDate";
        }
        else if (endDate.HasValue)
        {
            countQuery += " AND InvoiceDate <= @EndDate";
            query += " AND InvoiceDate <= @EndDate";
        }

        // Calculate total count
        int totalCount = await sqlConnection.ExecuteScalarAsync<int>(countQuery, new { CustomerId = customerId });

        // Calculate total pages
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        // Add pagination
        query += " ORDER BY InvoiceDate OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        int offset = (pageNumber - 1) * pageSize;

        IEnumerable<Invoice> result = await sqlConnection.QueryAsync<Invoice>(
            query,
            new { CustomerId = customerId, StartDate = startDate, EndDate = endDate, PageSize = pageSize, Offset = offset });

        return result.ToCustomList(totalCount, totalPages);
    }


    public async Task<CustomList<Invoice>> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber)
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


    public async Task<bool> IsInvoiceSerialExist(string invoiceSerial)
    {
        return await _context.Set<Invoice>().AnyAsync(a=>a.InvoiceSerial == invoiceSerial);
    }
}
