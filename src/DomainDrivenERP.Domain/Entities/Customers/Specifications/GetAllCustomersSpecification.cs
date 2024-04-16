using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.Customers.Specifications;
public static class GetAllCustomersSpecification
{
    public static BaseSpecification<Customer> GetAllCustomersSpec()
    {
        return new BaseSpecification<Customer>();
    }
}
