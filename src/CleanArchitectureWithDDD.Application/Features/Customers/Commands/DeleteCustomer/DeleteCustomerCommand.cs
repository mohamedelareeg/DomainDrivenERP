using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : ICommand<bool>
    {
    }
}
