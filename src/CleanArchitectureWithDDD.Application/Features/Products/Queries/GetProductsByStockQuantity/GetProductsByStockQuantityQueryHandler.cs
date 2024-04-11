using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Products;
using CleanArchitectureWithDDD.Domain.Shared.Results;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductsByStockQuantity;
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
