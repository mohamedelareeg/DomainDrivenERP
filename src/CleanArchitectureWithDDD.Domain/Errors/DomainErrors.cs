using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Domain.Errors;

public static class DomainErrors
{
    public static class CustomerErrors
    {
        public static readonly Error IsNulledCustomer = new Error("Customers.CreateInvoice", "Cannot create invoice for null customer.");
        public static readonly Error IsCustomerEmailAlreadyExist = new Error("Customer.CreateCustomer", "Email Already Exist");
    }
    public static class EmailErrors
    {
        public static readonly Error Empty = new Error("Email.Empty", "Email is Empty");
        public static readonly Error TooLong = new Error("Email.TooLong", "Email is Too Long");
        public static readonly Error NotValid = new Error("Email.InvalidFormat", "Email is not in a valid format");
    }
}
