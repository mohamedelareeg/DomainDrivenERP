using CleanArchitectureWithDDD.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.DomainEvents
{
    public abstract record DomainEvent(Guid Id) : IDomainEvent;
}
