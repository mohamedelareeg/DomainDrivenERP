using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Commands.CreateCustomer;

internal class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Customer>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateCustomerCommandHandler(ICustomerRespository customerRespository, IUnitOfWork unitOfWork)
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

        Result<FirstName> firstNameResult = FirstName.Create(request.FirstName);
        Result<LastName> lastNameResult = LastName.Create(request.LastName);
        Result<Email> emailResult = Email.Create(request.Email);
        if (firstNameResult.IsFailure || lastNameResult.IsFailure)
        {
            return Result.Failure<Customer>(new Error("Customer.CreateCustomer", "First name or Last Name is Not Valid"));
        }
        bool isEmailUnique = await _customerRespository.IsEmailUniqueAsync(emailResult.Value, cancellationToken);
        Result<Customer> customer = Customer.Create(//Achieve the 3 Principles
            Guid.NewGuid(),
            firstNameResult.Value,
            lastNameResult.Value,
            emailResult.Value,
            request.Phone,
            isEmailUnique);
        if (customer.IsFailure)
        {
            return Result.Failure<Customer>(customer.Error);
        }
        await _customerRespository.AddAsync(customer.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return customer;
    }
}
