using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Commands;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Handlers
{
    public class CustomersCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>, IRequestHandler<UpdateCustomerCommand, bool>, IRequestHandler<DeleteCustomerCommand, bool>
    {
        public async Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var firstNameResult = FirstName.Create(request.FirstName);
            var lastNameResult = LastName.Create(request.LastName);
            if (firstNameResult.IsFailure || lastNameResult.IsFailure)
            {
                return null;
            }
            return new Customer(Guid.NewGuid(), firstNameResult.Value, lastNameResult.Value, request.Email, request.Phone);
        }

        public Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
