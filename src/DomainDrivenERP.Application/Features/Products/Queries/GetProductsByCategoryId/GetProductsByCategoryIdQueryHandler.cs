using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Features.Products.Queries.GetProductsByCategoryId;
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
