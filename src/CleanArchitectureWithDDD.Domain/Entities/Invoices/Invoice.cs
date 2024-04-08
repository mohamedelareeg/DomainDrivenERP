using System.Text.Json.Serialization;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Invoices;

public class Invoice : BaseEntity
{
    //Constractor is Required for Dapper
    public Invoice()
    {
    }
    internal Invoice(
        Guid id,
        string invoiceSerial,
        DateTime invoiceDate,
        decimal invoiceAmount,
        decimal invoiceDiscount,
        decimal invoiceTax,
        decimal invoiceTotal,
        Guid customerId) : base(id)
    {
        InvoiceSerial = invoiceSerial;
        InvoiceDate = invoiceDate;
        InvoiceAmount = invoiceAmount;
        InvoiceDiscount = invoiceDiscount;
        InvoiceTax = invoiceTax;
        InvoiceTotal = invoiceTotal;
        InvoiceStatus = InvoiceStatus.Pending;
        CustomerId = customerId;
    }
    public Guid CustomerId { get; private set; }
    // [JsonIgnore]
    // public Customer Customer { get; private set; }
    public string InvoiceSerial { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public decimal InvoiceAmount { get; private set; }
    public decimal InvoiceDiscount { get; private set; }
    public decimal InvoiceTax { get; private set; }
    public decimal InvoiceTotal { get; private set; }
    public InvoiceStatus InvoiceStatus { get; private set; }
    public void UpdateInvoiceStatus(InvoiceStatus newStatus)
    {
        InvoiceStatus = newStatus;
    }
}
