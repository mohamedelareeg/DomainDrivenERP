using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;

public interface ICustomerRespository
{
    Task AddAsync(Customer customer);
    Task AddCustomerInvoiceAsync(Invoice invoice);
    Task<CustomList<Customer>?> GetAllCustomers(CancellationToken cancellationToken = default);
    Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default);
    Task Update(Customer customer);
    Task UpdateInvoiceStatus(Invoice invoiceUpdated);
}
