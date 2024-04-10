using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers.Specifications;
public static class GetCustomerByIdSpecification
{
    public static BaseSpecification<Customer> GetCustomerByIdSpec(string customerId)
    {
        return new BaseSpecification<Customer>(c => c.Id.ToString() == customerId);
    }
}
