using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Commands.UpdateCustomerInvoice;
public class UpdateCustomerInvoiceCommand : ICommand<Invoice>
{
    public UpdateCustomerInvoiceCommand(string invoiceId, decimal invoiceAmount)
    {
        InvoiceId = invoiceId;
        InvoiceAmount = invoiceAmount;
    }

    public string InvoiceId { get; }
    public decimal InvoiceAmount { get; }
}
