using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Entities.Customers.Specifications;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.Shared.Specifications;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Persistence.Repositories.Customers;
internal class CustomerSpecificationRepository : ICustomerRespository
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerSpecificationRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(Customer customer)
    {
        await _unitOfWork.Repository<Customer>().AddAsync(customer);
    }

    public async Task AddCustomerInvoiceAsync(Invoice invoice)
    {
        await _unitOfWork.Repository<Invoice>().AddAsync(invoice);
    }

    public async Task<CustomList<Customer>?> GetAllCustomers(CancellationToken cancellationToken = default)
    {
        BaseSpecification<Customer> spec = GetAllCustomersSpecification.GetAllCustomersSpec();
        return await _unitOfWork.Repository<Customer>().CustomListAsync(spec, false);
    }

    public async Task<Customer?> GetByIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        BaseSpecification<Customer> spec = GetCustomerByIdSpecification.GetCustomerByIdSpec(customerId);
        return await _unitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec, false, cancellationToken);
    }

    public async Task<Customer?> GetCustomerInvoicesById(string customerId, CancellationToken cancellationToken = default)
    {
        BaseSpecification<Customer> spec = GetCustomerInvoicesByIdSpecification.GetCustomerInvoicesByIdSpec(customerId);
        return await _unitOfWork.Repository<Customer>().FirstOrDefaultAsync(spec, false, cancellationToken);
    }

    public async Task<bool> IsEmailUniqueAsync(Email value, CancellationToken cancellationToken = default)
    {
        BaseSpecification<Customer> spec = IsEmailUniqueSpecification.IsEmailUniqueSpec(value);
        return !await _unitOfWork.Repository<Customer>().AnyAsync(spec, false, cancellationToken);
    }

    public async Task Update(Customer customer)
    {
        _unitOfWork.Repository<Customer>().Update(customer);
    }

    public async Task UpdateInvoiceStatus(Invoice invoiceUpdated)
    {
        _unitOfWork.Repository<Invoice>().Update(invoiceUpdated);
    }
}
