using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.DomainEvents
{
    public sealed record CreateCustomerDomainEvent(Guid Id,Guid CustomerId) : DomainEvent(Id)
    {
    }
}
