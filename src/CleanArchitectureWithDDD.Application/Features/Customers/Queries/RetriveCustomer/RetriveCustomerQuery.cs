using CleanArchitectureWithDDD.Application.Abstractions.Messaging;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer;

public class RetriveCustomerQuery : IQuery<RetriveCustomerResponse>
{
    public Guid CustomerId { get; }
    public RetriveCustomerQuery()
    {

    }
    public RetriveCustomerQuery(Guid customerId)
    {
        CustomerId = customerId;
    }
}
