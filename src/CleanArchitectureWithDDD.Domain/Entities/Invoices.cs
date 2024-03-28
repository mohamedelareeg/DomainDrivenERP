using CleanArchitectureWithDDD.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Entities
{
    public sealed class Invoices : BaseEntity
    {
        public Invoices(
            Guid id,
            string invoiceId,
            DateTime invoiceDate,
            decimal invoiceAmount,
            decimal invoiceDiscount,
            decimal invoiceTax,
            decimal invoiceTotal):base(id)
        {
            InvoiceId = invoiceId;
            InvoiceDate = invoiceDate;
            InvoiceAmount = invoiceAmount;
            InvoiceDiscount = invoiceDiscount;
            InvoiceTax = invoiceTax;
            InvoiceTotal = invoiceTotal;
        }
        public string InvoiceId { get; private set; }
        public DateTime InvoiceDate { get; private set; }
        public decimal InvoiceAmount { get; private set; }
        public decimal InvoiceDiscount { get; private set; }
        public decimal InvoiceTax { get; private set; }
        public decimal InvoiceTotal { get; private set; }
    }
}
