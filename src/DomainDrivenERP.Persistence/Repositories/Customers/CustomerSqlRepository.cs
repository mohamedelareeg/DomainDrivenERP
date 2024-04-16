using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;
using DomainDrivenERP.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenERP.Persistence.Repositories.Customers;
internal class CustomerSqlRepository : ICustomerRespository
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly SqlConnection _sqlConnection;

    public CustomerSqlRepository(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _sqlConnection = _connectionFactory.SqlConnection();
    }


    public async Task AddAsync(Customer customer)
    {
        CustomerSnapshot snapshot = customer.ToSnapShot();

        string sql = @"
            INSERT INTO Customers (Id, FirstName, LastName, Email, Phone)
            VALUES (@Id, @FirstName, @LastName, @Email, @Phone)";

        await _sqlConnection.ExecuteAsync(sql, snapshot);
    }

    public async Task AddCustomerInvoiceAsync(Invoice invoice)
    {
        InvoiceSnapshot snapshot = invoice.ToSnapshot();

        string sql = @"
        INSERT INTO Invoices (Id, CustomerId, InvoiceSerial, InvoiceDate, InvoiceAmount, InvoiceDiscount, InvoiceTax, InvoiceTotal, InvoiceStatus)
        VALUES (@Id, @CustomerId, @InvoiceSerial, @InvoiceDate, @InvoiceAmount, @InvoiceDiscount, @InvoiceTax, @InvoiceTotal, @InvoiceStatus)";

        await _sqlConnection.ExecuteAsync(sql, snapshot);
    }


    public async Task<CustomList<Customer>?> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        string sql = @"
        SELECT Id, FirstName, LastName, Email, Phone, CreatedOnUtc, ModifiedOnUtc
        FROM Customers";

        IEnumerable<CustomerSnapshot> results = await _sqlConnection.QueryAsync<CustomerSnapshot>(sql);

        var customers = results.Select(result => Customer.FromSnapshot(result)).ToCustomList();

        return customers;
    }


    public async Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default)
    {
        string sql = @"
        SELECT Id, FirstName, LastName, Email, Phone, CreatedOnUtc, ModifiedOnUtc
        FROM Customers
        WHERE Id = @Id";

        CustomerSnapshot? result = await _sqlConnection.QuerySingleOrDefaultAsync<CustomerSnapshot>(sql, new { Id = CustomerId });

        if (result is not null)
        {
            return Customer.FromSnapshot(result);
        }
        else
        {
            return null;
        }
    }

    public async Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default)
    {
        string sql = @"
        SELECT 
            c.Id, c.FirstName, c.LastName, c.Email, c.Phone, c.CreatedOnUtc, c.ModifiedOnUtc,
            i.Id AS InvoiceId, i.InvoiceSerial, i.InvoiceDate, 
            i.InvoiceAmount, i.InvoiceDiscount, i.InvoiceTax, i.InvoiceTotal, i.InvoiceStatus
        FROM Customers c
        LEFT JOIN Invoices i ON c.Id = i.CustomerId
        WHERE c.Id = @CustomerId";

        var customerDictionary = new Dictionary<Guid, Customer>();

        await _sqlConnection.QueryAsync<Customer, Invoice, Customer>(
              sql,
              (customer, invoice) =>
              {
                  if (!customerDictionary.TryGetValue(customer.Id, out Customer? cust))
                  {
                      cust = customer;
                      customerDictionary.Add(cust.Id, cust);
                  }
                  if (invoice != null)
                  {
                      cust.AddInvoice(invoice);
                  }
                  return cust;
              },
              new { CustomerId = customerId },
              splitOn: "InvoiceId");

        return customerDictionary.Values.FirstOrDefault();
    }
    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
        string sql = @"
        SELECT COUNT(*)
        FROM Customers
        WHERE Email = @Email";

        int count = await _sqlConnection.ExecuteScalarAsync<int>(sql, new { Email = value.Value });
        return count == 0;
    }


    public async Task Update(Customer customer)
    {
        CustomerSnapshot snapshot = customer.ToSnapShot();

        string sql = @"
        UPDATE Customers
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone
        WHERE Id = @Id";

        await _sqlConnection.ExecuteAsync(sql, snapshot);
    }
    public async Task UpdateInvoiceStatus(Invoice invoiceUpdated)
    {
        InvoiceSnapshot snapshot = invoiceUpdated.ToSnapshot();

        string sql = @"
        UPDATE Invoices
        SET InvoiceStatus = @InvoiceStatus
        WHERE Id = @Id";

        await _sqlConnection.ExecuteAsync(sql, snapshot);
    }

}
