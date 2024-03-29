using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.DomainEvents
{
    public sealed record UpdateInvoiceStatusDomainEvent(Guid CustomerId , Invoice Invoice , InvoiceStatus InvoiceStatus) : IDomainEvent
    {
    }
}
