using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Requests.Commands
{
    public class CreateCustomerCommand : IRequest<Result<Customer>>
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
}
