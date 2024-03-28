using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Commands;
using CleanArchitectureWithDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Handlers
{
    public class CustomersCommandHandler : IRequestHandler<CreateCustomerCommand, Customer>, IRequestHandler<UpdateCustomerCommand, bool>, IRequestHandler<DeleteCustomerCommand, bool>
    {
        public Task<Customer> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
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
