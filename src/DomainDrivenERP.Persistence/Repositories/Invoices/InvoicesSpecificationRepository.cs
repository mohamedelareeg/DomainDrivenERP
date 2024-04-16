using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using DomainDrivenERP.Application.Extentions;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Entities.Invoices.Specifications;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Persistence.Repositories.Invoices;
internal class InvoicesSpecificationRepository : IInvoiceRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public InvoicesSpecificationRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomList<Invoice>?> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        BaseSpecification<Invoice> spec = GetInvoicesByCustomerIdSpecification.GetInvoicesByCustomerIdSpec(customerId, startDate, endDate);

        int totalCount = await _unitOfWork.Repository<Invoice>().CountAsync(spec);
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        if (pageSize > 0)
        {
            spec.ApplyPaging((pageNumber - 1) * pageSize, pageSize);
        }
        IList<Invoice> data = await _unitOfWork.Repository<Invoice>().ListAsync(spec, false, cancellationToken);
        return data.ToCustomList(totalCount, totalPages);
    }

    public async Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default)
    {
        BaseSpecification<Invoice> spec = IsInvoiceSerialExistSpecification.IsInvoiceSerialExistSpec(invoiceSerial);
        return await _unitOfWork.Repository<Invoice>().AnyAsync(spec, false, cancellationToken);
    }
}
