using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Queries;
using CleanArchitectureWithDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Handlers
{
    public class CustomersQueryHandler : IRequestHandler<RetriveCustomerQuery, Customer>, IRequestHandler<RetriveCustomersQuery, List<Customer>>
    {
        public Task<Customer> Handle(RetriveCustomerQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Customer>> Handle(RetriveCustomersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
