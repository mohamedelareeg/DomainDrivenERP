using CleanArchitectureWithDDD.Application.Extentions;
using CleanArchitectureWithDDD.Application.Features.Customers.Queries.GetCustomerInvoicesById;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Customers.Specifications;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.Specifications;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using CleanArchitectureWithDDD.Persistence.Clients;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureWithDDD.Persistence.Repositories;

internal sealed class CustomerRespository : ICustomerRespository
{
    private readonly ApplicationDbContext _context;
    private readonly ISqlConnectionFactory _connectionFactory;
    public CustomerRespository(ApplicationDbContext context, ISqlConnectionFactory connectionFactory)
    {
        _context = context;
        _connectionFactory = connectionFactory;
    }
    private IQueryable<Customer> ApplySpecification(
        BaseSpecification<Customer> specification)
    {
        return SpecificationEvaluator.GetQuery(
            _context.Set<Customer>(),
            specification);
    }
    public async Task AddAsync(Customer customer)
    {
        await _context.Set<Customer>().AddAsync(customer);
    }

    public async Task AddCustomerInvoiceAsync(Invoice invoice)
    {
        await _context.Set<Invoice>().AddAsync(invoice);
    }

    public async Task<CustomList<Customer>> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        return _context.Set<Customer>().ToCustomList();//TODO Fix the Async
    }

    public async Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Customer>().FirstOrDefaultAsync(x => x.Id.ToString() == CustomerId, cancellationToken);
    }

    public async Task<dynamic?> GetByIdAsync_Dapper(Guid customerId)
    {
        await using SqlConnection sqlConnection = _connectionFactory.SqlConnection();
        dynamic? result = await sqlConnection.QueryFirstOrDefaultAsync<dynamic>(
            @"SELECT Id, FirstName, LastName, Email, Phone FROM Customers WHERE Id = @CustomerId",
            new { CustomerId = customerId }
        );
        return result;
    }

    public async Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default)
    {
        // Ordinary way of retrieving customer and invoices
        // return await _context.Set<Customer>().Where(a => a.Id.ToString() == customerId).Include(a => a.Invoices).SingleOrDefaultAsync(cancellationToken);

        // Using the Specification Design Pattern to retrieve customer and invoices
        return await ApplySpecification(new GetCustomerInvoicesByIdSpecifications(customerId)).FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
        return !await _context.Set<Customer>().AnyAsync(x => x.Email == value, cancellationToken);
    }

    public Task UpdateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task UpdateInvoiceStatusAsync(Invoice invoiceUpdated)
    {
        throw new NotImplementedException();
    }
}
