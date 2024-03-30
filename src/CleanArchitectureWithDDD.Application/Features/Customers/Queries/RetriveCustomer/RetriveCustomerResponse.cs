namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer;

public sealed record RetriveCustomerResponse(Guid Id, string FirstName, string LastName, string Email, string Phone)
{
    // Empty constructor required by AutoMapper
    public RetriveCustomerResponse() : this(Guid.Empty, "", "", "", "") { }
}
