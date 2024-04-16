using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Products.Queries.GetProductsByStockQuantity;
public class GetProductsByStockQuantityQueryHandler : IListQueryHandler<GetProductsByStockQuantityQuery, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByStockQuantityQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<CustomList<Product>>> Handle(GetProductsByStockQuantityQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsByStockQuantityAsync(request.Quantity, cancellationToken);
    }
}
