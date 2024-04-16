namespace DomainDrivenERP.Domain.Exceptions;

public sealed class CreateInvoiceOfCustomerIsNullDomainException : DomainException
{
    public CreateInvoiceOfCustomerIsNullDomainException(string message) : base(message) { }
}
