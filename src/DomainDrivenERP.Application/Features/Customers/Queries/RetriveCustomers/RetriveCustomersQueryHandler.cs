using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Customers.Queries.RetriveCustomers;

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
