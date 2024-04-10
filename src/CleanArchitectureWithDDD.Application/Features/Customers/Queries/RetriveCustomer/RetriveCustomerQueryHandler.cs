using AutoMapper;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Customers.Queries.RetriveCustomer;

internal class RetriveCustomerQueryHandler : IQueryHandler<RetriveCustomerQuery, RetriveCustomerResponse>
{
    private readonly ICustomerRespository _customerRespository;
    private readonly IMapper _mapper;

    public RetriveCustomerQueryHandler(ICustomerRespository customerRespository, IMapper mapper)
    {
        _customerRespository = customerRespository;
        _mapper = mapper;
    }
    public async Task<Result<RetriveCustomerResponse>> Handle(RetriveCustomerQuery request, CancellationToken cancellationToken)
    {
        Customer? customer = await _customerRespository.GetByIdAsync(request.CustomerId.ToString(), cancellationToken);

        if (customer is null)
        {
            return Result.Failure<RetriveCustomerResponse>(new Error("Customer.RetriveCustomer", "Customer doesn't Exist"));
        }
        return _mapper.Map<RetriveCustomerResponse>(customer);
    }
}
