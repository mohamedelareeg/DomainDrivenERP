using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer
{
    internal class RetriveCustomerQueryHandler : IQueryHandler<RetriveCustomerQuery, Customer>
    {
        private ICustomerRespository _customerRespository;
        public RetriveCustomerQueryHandler(ICustomerRespository customerRespository)
        {
            _customerRespository = customerRespository;
        }
        public async Task<Result<Customer>> Handle(RetriveCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(request.CustomerId, cancellationToken);
            if (customer is null)
            {
                return Result.Failure<Customer>(new Error("Customer.RetriveCustomer", "Customer doesn't Exist"));
            }
            return customer;
        }

    }
}
