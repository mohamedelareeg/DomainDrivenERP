using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence.Repositories
{
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

        public Task AddCustomerInvoiceAsync(Guid id, Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetAllCustomers(CancellationToken cancellationToken)
        {
            return await _context.Set<Customer>().ToListAsync(cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid CustomerId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Customer>().FirstOrDefaultAsync(x => x.Id == CustomerId, cancellationToken);
        }

        public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken)
        {
            return !await _context.Set<Customer>().AnyAsync(x=>x.Email==value, cancellationToken);
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
}
