using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Products;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductsByCategoryId;
public class GetProductsByCategoryIdQueryHandler : IListQueryHandler<GetProductsByCategoryIdQuery, Product>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<CustomList<Product>>> Handle(GetProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsByCategoryIdAsync(request.CategoryId, request.FromDate, request.ToDate, cancellationToken);
    }
}
