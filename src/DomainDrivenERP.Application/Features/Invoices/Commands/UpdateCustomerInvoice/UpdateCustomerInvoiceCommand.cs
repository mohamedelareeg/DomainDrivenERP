using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Invoices;

namespace DomainDrivenERP.Application.Features.Invoices.Commands.UpdateCustomerInvoice;
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
