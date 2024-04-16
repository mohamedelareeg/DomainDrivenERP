using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Application.Features.Customers.Queries.GetCustomerInvoicesById;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Customers.Specifications;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;
using DomainDrivenERP.Persistence.Clients;
using DomainDrivenERP.Persistence.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DomainDrivenERP.Persistence.Repositories.Customers;

internal sealed class CustomerRespository : ICustomerRespository
{
    private readonly ApplicationDbContext _context;
    public CustomerRespository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Customer customer)
    {
        await _context.Set<Customer>().AddAsync(customer);
    }

    public async Task AddCustomerInvoiceAsync(Invoice invoice)
    {
        await _context.Set<Invoice>().AddAsync(invoice);
    }

    public async Task<CustomList<Customer>?> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        return _context.Set<Customer>().ToCustomList(); // TODO Fix the Async
    }

    public async Task<Customer?> GetByIdAsync(string CustomerId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Customer>().FirstOrDefaultAsync(x => x.Id.ToString() == CustomerId, cancellationToken);
    }

    public async Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default)
    {
         return await _context.Set<Customer>().Where(a => a.Id.ToString() == customerId).Include(a => a.Invoices).SingleOrDefaultAsync(cancellationToken);
    }
    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
        return !await _context.Set<Customer>().AnyAsync(x => x.Email == value, cancellationToken);
    }

    public async Task Update(Customer customer)
    {
        _context.Set<Customer>().Update(customer);
    }

    public async Task UpdateInvoiceStatus(Invoice invoiceUpdated)
    {
         _context.Set<Invoice>().Update(invoiceUpdated);
    }
}
