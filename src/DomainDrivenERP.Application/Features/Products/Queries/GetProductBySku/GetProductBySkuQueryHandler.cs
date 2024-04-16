using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Products.Queries.GetProductBySku;
public class GetProductBySkuQueryHandler : IQueryHandler<GetProductBySkuQuery, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductBySkuQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(GetProductBySkuQuery request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetProductBySkuAsync(request.SKU, cancellationToken);
        if (product == null)
        {
            return Result.Failure<Product>("Product.GetProductBySku", "Product not found");
        }

        return Result.Success(product);
    }
}
