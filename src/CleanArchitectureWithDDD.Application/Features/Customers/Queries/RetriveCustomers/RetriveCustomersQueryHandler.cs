using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomers;

internal class RetriveCustomersQueryHandler : IListQueryHandler<RetriveCustomersQuery, Customer>
{
    private readonly ICustomerRespository _customerRespository;
    public RetriveCustomersQueryHandler(ICustomerRespository customerRespository)
    {
        _customerRespository = customerRespository;
    }

    public async Task<Result<CustomList<Customer>>> Handle(RetriveCustomersQuery request, CancellationToken cancellationToken)
    {
        CustomList<Customer>? customers = await _customerRespository.GetAllCustomers(cancellationToken);
        return customers is null || customers.Count == 0
            ? Result.Failure<CustomList<Customer>>(new Error("Customer.RetriveCustomers", "No Customers Exist"))
            : (Result<CustomList<Customer>>)customers;
    }
}
