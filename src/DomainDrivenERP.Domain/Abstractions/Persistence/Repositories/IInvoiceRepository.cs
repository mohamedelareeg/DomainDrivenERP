using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Invoices;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
public interface IInvoiceRepository
{
    Task<CustomList<Invoice>?> GetAllCustomerInvoices(string customerId, DateTime? startDate, DateTime? endDate, int pageSize, int pageNumber, CancellationToken cancellationToken = default);
    Task<bool> IsInvoiceSerialExist(string invoiceSerial, CancellationToken cancellationToken = default);
}
