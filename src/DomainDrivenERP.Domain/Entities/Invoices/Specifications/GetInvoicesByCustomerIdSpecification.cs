using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.Invoices.Specifications;
public static class GetInvoicesByCustomerIdSpecification
{
    public static BaseSpecification<Invoice> GetInvoicesByCustomerIdSpec(string customerId, DateTime? startDate, DateTime? endDate)
    {
        var spec = new BaseSpecification<Invoice>(
            i => i.CustomerId.ToString() == customerId
        );

        if (startDate.HasValue)
        {
            spec.ApplyWhere(i => i.InvoiceDate >= startDate);
        }

        if (endDate.HasValue)
        {
            spec.ApplyWhere(i => i.InvoiceDate <= endDate);
        }
        return spec;
    }
}
