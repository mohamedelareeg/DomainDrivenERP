using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Domain.Entities.Customers.Specifications;
public static class IsEmailUniqueSpecification
{
    public static BaseSpecification<Customer> IsEmailUniqueSpec(Email value)
    {
        return new BaseSpecification<Customer>(c => c.Email == value);
    }
}
