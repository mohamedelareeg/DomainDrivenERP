using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;

namespace CleanArchitectureWithDDD.RepositoriesPerformance.Repositories.InvoiceRepository;

public class GetAllCustomerInvoicesBenchmark
{
    private readonly ISqlConnectionFactory _connectionFactory;
    private readonly IInvoiceRepository _invoiceRepository;
    private string _customerId;

    public GetAllCustomerInvoicesBenchmark(IInvoiceRepository invoiceRepository, ISqlConnectionFactory connectionFactory)
    {
        _invoiceRepository = invoiceRepository;
        _connectionFactory = connectionFactory;
    }

    [GlobalSetup]
    public async Task GlobalSetup()
    {
         //There is no argument given that corresponds to the required parameter 'invoiceRepository' of 'GetAllCustomerInvoicesBenchmark.GetAllCustomerInvoicesBenchmark(IInvoiceRepository, ISqlConnectionFactory
        _customerId = "a7ee9212-62d7-4f1d-8de8-0dcc7958c40f"; // In my case i test with this customerId
    }

    [Benchmark]
    public async Task<CustomList<Invoice>> GetAllCustomerInvoices()
    {
        return await _invoiceRepository.GetAllCustomerInvoices(_customerId, null, null , 10 , 1);
    }

    [Benchmark]
    public async Task<CustomList<Invoice>> GetAllCustomerInvoicesWithDapper()
    {
        await using Microsoft.Data.SqlClient.SqlConnection sqlConnection = _connectionFactory.SqlConnection();
        IEnumerable<Invoice> result = await sqlConnection.QueryAsync<Invoice>(
            @"SELECT * FROM Invoices WHERE CustomerId = @CustomerId",
            new { CustomerId = _customerId });

        return result.ToCustomList();
    }
}
