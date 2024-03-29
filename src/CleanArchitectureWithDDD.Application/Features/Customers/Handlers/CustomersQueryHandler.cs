using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Queries;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitectureWithDDD.Domain.Errors.DomainErrors;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Handlers
{
    public class CustomersQueryHandler : IRequestHandler<RetriveCustomerQuery, Result<Customer>>, IRequestHandler<RetriveCustomersQuery, Result<List<Customer>>>
    {
        private ICustomerRespository _customerRespository;
        public CustomersQueryHandler(ICustomerRespository customerRespository)
        {
            _customerRespository = customerRespository;
        }
        public async Task<Result<Customer>> Handle(RetriveCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRespository.GetByIdAsync(request.CustomerId,cancellationToken);
            if(customer is null)
            {
                return Result.Failure<Customer>(new Error("Customer.RetriveCustomer", "Customer doesn't Exist"));
            }
            return customer;
        }

        public async Task<Result<List<Customer>>> Handle(RetriveCustomersQuery request, CancellationToken cancellationToken)
        {
            List<Customer> customers = await _customerRespository.GetAllCustomers(cancellationToken);
            if (customers is null || customers.Count == 0)
            {
                return Result.Failure<List<Customer>>(new Error("Customer.RetriveCustomers", "No Customers Exist"));
            }
            return customers;
        }
    }
}
