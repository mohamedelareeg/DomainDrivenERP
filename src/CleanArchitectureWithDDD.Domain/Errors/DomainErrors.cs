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
        public static class Email
        {
            public static readonly Error Empty = new Error("Email.Empty", "Email is Empty");
            public static readonly Error TooLong = new Error("Email.TooLong", "Email is Too Long");
            public static readonly Error NotValid = new Error("Email.InvalidFormat", "Email is not in a valid format");
        }
    }
}
