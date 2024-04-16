using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Application.Features.Products.Commands.ApplyDiscount;
public class ApplyDiscountCommandHandler : ICommandHandler<ApplyDiscountCommand, decimal>
{
    private readonly IProductRepository _productRepository;

    public ApplyDiscountCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<decimal>> Handle(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Products.Product? product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return Result.Failure<decimal>("Product.ApplyDiscount", $"Product with ID {request.ProductId} not found.");
        }

        Result<decimal> discountResult = product.ApplyDiscount(request.DiscountPercentage);
        if (discountResult.IsFailure)
        {
            return Result.Failure<decimal>(discountResult.Error);
        }

        return discountResult;
    }
}
