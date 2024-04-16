using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.Invoices.Specifications;
public static class IsInvoiceSerialExistSpecification
{
    public static BaseSpecification<Invoice> IsInvoiceSerialExistSpec(string invoiceSerial)
    {
        return new BaseSpecification<Invoice>(i => i.InvoiceSerial == invoiceSerial);
    }
}
