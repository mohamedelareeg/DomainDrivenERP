using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Enums;

namespace DomainDrivenERP.Domain.Entities.Invoices;
public class InvoiceSnapshot
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string InvoiceSerial { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal InvoiceAmount { get; set; }
    public decimal InvoiceDiscount { get; set; }
    public decimal InvoiceTax { get; set; }
    public decimal InvoiceTotal { get; set; }
    public InvoiceStatus InvoiceStatus { get; set; }
}
