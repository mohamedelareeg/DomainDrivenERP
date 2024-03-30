using AutoMapper;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
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
    internal class RetriveCustomerQueryHandler : IQueryHandler<RetriveCustomerQuery, RetriveCustomerResponse>
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IMapper _mapper;

        public RetriveCustomerQueryHandler(ICustomerRespository customerRespository, IMapper mapper)
        {
            _customerRespository = customerRespository;
            _mapper = mapper;
        }
        public async Task<Result<RetriveCustomerResponse>> Handle(RetriveCustomerQuery request, CancellationToken cancellationToken)
        {
            // Scenario 1: Retrieving customer information using Dapper Micro ORM
            var customer = await _customerRespository.GetByIdAsync_Dapper(request.CustomerId);

            // Scenario 2: Retrieving customer information using Entity Framework ORM
            //var customer = await _customerRespository.GetByIdAsync(request.CustomerId, cancellationToken); // Entity FrameWork ORM
            
            /*
             * In my test:
             * - Dapper takes 4ms to retrieve the customer.
             * - Entity Framework takes 7ms to retrieve the customer.
             */

            if (customer is null)
            {
                return Result.Failure<RetriveCustomerResponse>(new Error("Customer.RetriveCustomer", "Customer doesn't Exist"));
            }

            // Map the retrieved customer to RetriveCustomerResponse using AutoMapper
            return _mapper.Map<RetriveCustomerResponse>(customer);
        }

      

    }
}
