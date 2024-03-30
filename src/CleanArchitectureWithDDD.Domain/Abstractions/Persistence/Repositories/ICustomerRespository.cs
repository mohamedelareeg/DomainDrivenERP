using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories
{
    public interface ICustomerRespository
    {
        Task AddAsync(Customer customer);
        Task AddCustomerInvoiceAsync(Guid id, Invoice invoice);
        Task<List<Customer>> GetAllCustomers(CancellationToken cancellationToken = default);
        Task<Customer?> GetByIdAsync(Guid CustomerId, CancellationToken cancellationToken = default);
        Task<dynamic?> GetByIdAsync_Dapper(Guid customerId);
        Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default);
        Task UpdateAsync(Customer customer);
        Task UpdateInvoiceStatusAsync(Invoice invoiceUpdated);
    }
}
