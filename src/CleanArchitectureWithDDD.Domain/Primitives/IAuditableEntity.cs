using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Primitives
{
    public interface IAuditableEntity
    {
        DateTime CreatedOnUtc { get; set; }

        DateTime? ModifiedOnUtc { get; set; }
    }
}
