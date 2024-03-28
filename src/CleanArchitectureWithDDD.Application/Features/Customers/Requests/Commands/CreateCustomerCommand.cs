using CleanArchitectureWithDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Requests.Commands
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
    }
}
