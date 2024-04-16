using System.Text.Json.Serialization;
using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;

namespace DomainDrivenERP.Domain.Entities.Invoices;

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
        Guard.Against.NullOrEmpty(id.ToString(), nameof(id));
        Guard.Against.NullOrWhiteSpace(invoiceSerial, nameof(invoiceSerial));
        Guard.Against.NullOrEmpty(invoiceDate.ToString(), nameof(invoiceDate));
        Guard.Against.NumberNegativeOrZero(invoiceAmount, nameof(invoiceAmount));
        Guard.Against.NumberNegativeOrZero(invoiceDiscount, nameof(invoiceDiscount));
        Guard.Against.NumberNegativeOrZero(invoiceTax, nameof(invoiceTax));
        Guard.Against.NumberNegativeOrZero(invoiceTotal, nameof(invoiceTotal));

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
        Guard.Against.Null(newStatus, nameof(newStatus));

        InvoiceStatus = newStatus;
    }
    public InvoiceSnapshot ToSnapshot()
    {
        return new InvoiceSnapshot
        {
            Id = Id,
            CustomerId = CustomerId,
            InvoiceSerial = InvoiceSerial,
            InvoiceDate = InvoiceDate,
            InvoiceAmount = InvoiceAmount,
            InvoiceDiscount = InvoiceDiscount,
            InvoiceTax = InvoiceTax,
            InvoiceTotal = InvoiceTotal,
            InvoiceStatus = InvoiceStatus
        };
    }

    public static Invoice FromSnapshot(InvoiceSnapshot snapshot)
    {
        return new Invoice(
            snapshot.Id,
            snapshot.InvoiceSerial,
            snapshot.InvoiceDate,
            snapshot.InvoiceAmount,
            snapshot.InvoiceDiscount,
            snapshot.InvoiceTax,
            snapshot.InvoiceTotal,
            snapshot.CustomerId)
        {
            InvoiceStatus = snapshot.InvoiceStatus
        };
    }

}
