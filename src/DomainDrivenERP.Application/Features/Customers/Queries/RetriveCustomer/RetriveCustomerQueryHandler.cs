using AutoMapper;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Customers.Queries.RetriveCustomer;

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
