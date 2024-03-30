using CleanArchitectureWithDDD.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Customers
        {
            public static readonly Error IsNulledCustomer = new Error("Customers.CreateInvoice", "Cannot create invoice for null customer.");
            public static readonly Error IsCustomerEmailAlreadyExist = new Error("Customer.CreateCustomer", "Email Already Exist");
        }
    }
}
