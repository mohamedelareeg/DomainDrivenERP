using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Primitives
{
    //Outbox Pattern
    //Domain Event Pattern
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
    }

}
