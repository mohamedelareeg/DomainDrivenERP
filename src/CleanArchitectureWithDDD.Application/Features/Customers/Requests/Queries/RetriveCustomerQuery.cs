using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Requests.Queries
{
    public class RetriveCustomerQuery :IRequest<Result<Customer>>
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
