using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers.Specifications;
public class GetCustomerInvoicesByIdSpecifications : BaseSpecification<Customer>
{
    public GetCustomerInvoicesByIdSpecifications(string customerId)
       : base(a => a.Id.ToString() == customerId)
    {
        AddInclude(a => a.Invoices);
    }

}
