using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
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
    public async Task AddAsync(Customer customer)
    {
        await _context.Set<Customer>().AddAsync(customer);
    }

    public Task AddCustomerInvoiceAsync(Guid id, Invoice invoice)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Customer>> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Customer>().ToListAsync(cancellationToken);
    }

    public async Task<Customer?> GetByIdAsync(Guid CustomerId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Customer>().FirstOrDefaultAsync(x => x.Id == CustomerId, cancellationToken);
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
    /*
     public async Task<Customer?> GetByIdAsync_Dapper(Guid customerId)
    {
        await using SqlConnection sqlConnection = _connectionFactory.SqlConnection();
        var result = await sqlConnection.QueryFirstOrDefaultAsync<dynamic>(
            @"SELECT Id, FirstName, LastName, Email, Phone FROM Customers WHERE Id = @CustomerId",
            new { CustomerId = customerId }
        );
        if (result != null)
        {


            var firstNameResult = FirstName.Create(result.firstName);
            var lastNameResult = LastName.Create(result.lastName);
            var emailResult = Email.Create(result.email);

            if (firstNameResult.IsSuccess && lastNameResult.IsSuccess && emailResult.IsSuccess)
            {
                return Customer.Create(result.Id, firstNameResult.Value, lastNameResult.Value, emailResult.Value, result.Phone);
            }
            else
            {
                // Handle failure cases here, such as logging errors or returning null
                return null;
            }
        }
        else
        {
            return null;
        }
    }
     */

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
