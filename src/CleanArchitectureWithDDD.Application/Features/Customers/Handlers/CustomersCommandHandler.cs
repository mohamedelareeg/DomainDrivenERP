using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
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
    public class CustomersCommandHandler : ICommandHandler<CreateCustomerCommand, Customer>, ICommandHandler<UpdateCustomerCommand, bool>, ICommandHandler<DeleteCustomerCommand, bool>
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
            // Domain Model Principles:
            // 1. Completeness: All the imported business logic is completely encapsulated inside the Domain.
            // 2. Purity: Our Domain Model doesn't reach out of processing dependencies (e.g., Repositories) to perform business logic.
            // 3. Performance: Ensuring efficient processing within the Domain Model.

            var firstNameResult = FirstName.Create(request.FirstName);
            var lastNameResult = LastName.Create(request.LastName);
            var emailResult = Email.Create(request.Email);
            if (firstNameResult.IsFailure || lastNameResult.IsFailure)
            {
                return Result.Failure<Customer>(new Error("Customer.CreateCustomer","First name or Last Name is Not Valid"));
            }
            bool isEmailUnique = await _customerRespository.IsEmailUniqueAsync(emailResult.Value, cancellationToken);
            var customer = Customer.Create(//Achieve the 3 Principles
                Guid.NewGuid(),
                firstNameResult.Value,
                lastNameResult.Value,
                emailResult.Value,
                request.Phone,
                isEmailUnique);
            if(customer.IsFailure)
            {
                return Result.Failure<Customer>(customer.Error);
            }
            await _customerRespository.AddAsync(customer.Value);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return customer;
        }

        public Task<Result<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
