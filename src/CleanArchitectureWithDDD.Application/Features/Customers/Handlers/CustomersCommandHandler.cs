using CleanArchitectureWithDDD.Application.Features.Customers.Requests.Commands;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
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
    public class CustomersCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Customer>>, IRequestHandler<UpdateCustomerCommand, bool>, IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRespository _customerRespository;
        private readonly IUnitOfWork _unitOfWork;
        public CustomersCommandHandler(ICustomerRespository customerRespository , IUnitOfWork unitOfWork)
        {
            _customerRespository = customerRespository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Customer>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var firstNameResult = FirstName.Create(request.FirstName);
            var lastNameResult = LastName.Create(request.LastName);
            var emailResult = Email.Create(request.Email);
            if (firstNameResult.IsFailure || lastNameResult.IsFailure)
            {
                return null;
            }
            if (!await _customerRespository.IsEmailUniqueAsync(emailResult.Value, cancellationToken)) return Result.Failure<Customer>(new Error("Customer.CreateCustomer", "Email Already Exist"));
            var customer = Customer.Create(Guid.NewGuid(), firstNameResult.Value, lastNameResult.Value, emailResult.Value, request.Phone);
            await _customerRespository.AddAsync(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return customer;
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
