using CleanArchitectureWithDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer
{
    public sealed record RetriveCustomerResponse(Guid Id, string FirstName, string LastName, string Email, string Phone)
    {
        // Empty constructor required by AutoMapper
        public RetriveCustomerResponse() : this(Guid.Empty, "", "", "", "") { }
    }

}
