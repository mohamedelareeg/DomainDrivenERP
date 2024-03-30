using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomers;

internal class RetriveCustomersQueryHandler : IQueryHandler<RetriveCustomersQuery, List<Customer>>
{
    private readonly ICustomerRespository _customerRespository;
    public RetriveCustomersQueryHandler(ICustomerRespository customerRespository)
    {
        _customerRespository = customerRespository;
    }

    public async Task<Result<List<Customer>>> Handle(RetriveCustomersQuery request, CancellationToken cancellationToken)
    {
        List<Customer> customers = await _customerRespository.GetAllCustomers(cancellationToken);
        return customers is null || customers.Count == 0
            ? Result.Failure<List<Customer>>(new Error("Customer.RetriveCustomers", "No Customers Exist"))
            : (Result<List<Customer>>)customers;
    }
}
