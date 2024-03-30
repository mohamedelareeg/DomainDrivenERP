using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.DomainEvents
{
    //Record is Immutable
    public sealed record CreateInvoiceDomainEvent(Guid Id,Guid CustomerId , Invoice Invoice) : DomainEvent(Id)
    {
    }
}
