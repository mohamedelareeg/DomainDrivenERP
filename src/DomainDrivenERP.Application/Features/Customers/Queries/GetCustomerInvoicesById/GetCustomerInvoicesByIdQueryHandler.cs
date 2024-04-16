using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Customers;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Customers.Queries.GetCustomerInvoicesById;
internal class GetCustomerInvoicesByIdQueryHandler : IQueryHandler<GetCustomerInvoicesByIdQuery, Customer>
{
    private readonly ICustomerRespository _customerRespository;

    public GetCustomerInvoicesByIdQueryHandler(ICustomerRespository customerRespository)
    {
        _customerRespository = customerRespository;
    }

    public async Task<Result<Customer>> Handle(GetCustomerInvoicesByIdQuery request, CancellationToken cancellationToken)
    {
       return await _customerRespository.GetCustomerInvoicesById(request.CustomerId, cancellationToken);
    }
}
