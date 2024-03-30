using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Domain.DomainEvents;

//Record is Immutable
public sealed record CreateInvoiceDomainEvent(Guid Id, Guid CustomerId, Invoice Invoice) : DomainEvent(Id)
{
}
