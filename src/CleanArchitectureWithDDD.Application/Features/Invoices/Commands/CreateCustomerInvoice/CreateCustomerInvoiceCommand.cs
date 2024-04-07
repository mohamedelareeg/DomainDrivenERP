using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Commands.CreateCustomerInvoice;

public class CreateCustomerInvoiceCommand : ICommand<Invoice>
{
    public string CustomerId { get; }
    public string InvoiceSerial { get; }
    public DateTime InvoiceDate { get; }
    public decimal InvoiceAmount { get; }
    public CreateCustomerInvoiceCommand(string customerId, string invoiceSerial, DateTime invoiceDate, decimal invoiceAmount)
    {
        CustomerId = customerId;
        InvoiceSerial = invoiceSerial;
        InvoiceDate = invoiceDate;
        InvoiceAmount = invoiceAmount;
    }
}
