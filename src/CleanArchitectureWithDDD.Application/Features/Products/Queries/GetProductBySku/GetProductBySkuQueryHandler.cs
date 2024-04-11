using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Products;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductBySku;
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
