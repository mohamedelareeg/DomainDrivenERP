using DomainDrivenERP.Application.Abstractions.Messaging;

namespace DomainDrivenERP.Application.Features.Invoices.Commands.UpdateCustomerInvoiceStatus;

public class UpdateCustomerInvoiceStatusCommand : ICommand<bool>
{
    public UpdateCustomerInvoiceStatusCommand(Guid customerId, Guid invoiceId)
    {
        CustomerId = customerId;
        InvoiceId = invoiceId;
    }

    public Guid CustomerId { get; }
    public Guid InvoiceId { get; }
}
