using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.Customers;

namespace DomainDrivenERP.Application.Features.Customers.Queries.GetCustomerInvoicesById;
public class GetCustomerInvoicesByIdQuery : IQuery<Customer>
{
    public GetCustomerInvoicesByIdQuery(string customerId)
    {
        CustomerId = customerId;
    }

    public string CustomerId { get; }
}
