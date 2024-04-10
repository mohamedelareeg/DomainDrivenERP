using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;

public interface ICustomerRespository
{
    Task AddAsync(Customer customer);
    Task AddCustomerInvoiceAsync(Invoice invoice);
    Task<CustomList<Customer>?> GetAllCustomers(CancellationToken cancellationToken = default);
    Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer);
    Task UpdateInvoiceStatusAsync(Invoice invoiceUpdated);
}
