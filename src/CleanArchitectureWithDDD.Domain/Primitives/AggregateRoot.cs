using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Primitives
{
    public abstract class AggregateRoot : BaseEntity
    {
        protected AggregateRoot(Guid id):base(id) { }
    }
}
