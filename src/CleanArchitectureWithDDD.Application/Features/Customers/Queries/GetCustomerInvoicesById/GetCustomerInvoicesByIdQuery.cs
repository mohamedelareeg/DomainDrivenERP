using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Customers;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.GetCustomerInvoicesById;
public class GetCustomerInvoicesByIdQuery : IQuery<Customer>
{
    public GetCustomerInvoicesByIdQuery(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; }
}
