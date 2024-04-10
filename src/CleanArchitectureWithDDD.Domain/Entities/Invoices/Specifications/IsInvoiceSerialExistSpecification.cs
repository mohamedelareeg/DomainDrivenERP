using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.Invoices.Specifications;
public static class IsInvoiceSerialExistSpecification
{
    public static BaseSpecification<Invoice> IsInvoiceSerialExistSpec(string invoiceSerial)
    {
        return new BaseSpecification<Invoice>(i => i.InvoiceSerial == invoiceSerial);
    }
}
