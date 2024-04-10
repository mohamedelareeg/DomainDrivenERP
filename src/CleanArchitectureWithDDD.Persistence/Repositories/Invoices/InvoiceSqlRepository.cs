using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace CleanArchitectureWithDDD.Persistence.Repositories.Invoices;
internal class InvoiceSqlRepository : IInvoiceRepository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly SqlConnection _sqlConnection;

    public InvoiceSqlRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _sqlConnection = _connectionFactory.SqlConnection();
    }

    public async Task<CustomList<Invoice>?> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {

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
        int totalCount = await _sqlConnection.ExecuteScalarAsync<int>(countQuery, new { CustomerId = customerId });

        // Calculate total pages
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        // Add pagination
        query += " ORDER BY InvoiceDate OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
        int offset = (pageNumber - 1) * pageSize;

        IEnumerable<Invoice> result = await _sqlConnection.QueryAsync<Invoice>(
            query,
            new { CustomerId = customerId, StartDate = startDate, EndDate = endDate, PageSize = pageSize, Offset = offset });

        return result.ToCustomList(totalCount, totalPages);
    }

    public async Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default)
    {
        string sql = "SELECT TOP 1 Id FROM Invoices WHERE InvoiceSerial = @InvoiceSerial";
        Guid? result = await _sqlConnection.ExecuteScalarAsync<Guid?>(sql, new { InvoiceSerial = invoiceSerial });
        return result != null;
    }

}
