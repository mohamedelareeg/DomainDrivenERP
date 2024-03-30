using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer
{
    public class RetriveCustomerQuery : IQuery<Customer>
    {
        public Guid CustomerId { get; }
        public RetriveCustomerQuery()
        {

        }
        public RetriveCustomerQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}
