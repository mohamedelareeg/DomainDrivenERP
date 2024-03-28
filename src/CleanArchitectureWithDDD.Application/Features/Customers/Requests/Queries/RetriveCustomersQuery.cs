using CleanArchitectureWithDDD.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Requests.Queries
{
    public class RetriveCustomersQuery : IRequest<List<Customer>>
    {
    }
}
