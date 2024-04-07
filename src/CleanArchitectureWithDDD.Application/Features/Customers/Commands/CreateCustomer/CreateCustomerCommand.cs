using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities.Customers;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : ICommand<Customer>
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Phone { get; }
    public string Email { get; }
    public CreateCustomerCommand(string firstName, string lastName, string phone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;

    }
}
