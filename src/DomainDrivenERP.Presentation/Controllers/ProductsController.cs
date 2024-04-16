using System;
using System.Threading;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Features.Products.Commands.AddProduct;
using DomainDrivenERP.Application.Features.Products.Commands.ApplyDiscount;
using DomainDrivenERP.Application.Features.Products.Commands.UpdateProductName;
using DomainDrivenERP.Application.Features.Products.Commands.UpdateProductPrice;
using DomainDrivenERP.Application.Features.Products.Queries.GetProductById;
using DomainDrivenERP.Application.Features.Products.Queries.GetProductBySku;
using DomainDrivenERP.Application.Features.Products.Queries.GetProductsByCategoryId;
using DomainDrivenERP.Application.Features.Products.Queries.GetProductsByStockQuantity;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Presentation.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomainDrivenERP.Presentation.Controllers;

[Route("api/v1/products")]
public class ProductsController : AppControllerBase
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddProduct(AddProductCommand request, CancellationToken cancellationToken)
    {
        Result<Product> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPost("apply-discount")]
    public async Task<IActionResult> ApplyDiscount(ApplyDiscountCommand request, CancellationToken cancellationToken)
    {
        Result<decimal> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPut("update-name")]
    public async Task<IActionResult> UpdateProductName(UpdateProductNameCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpPut("update-price")]
    public async Task<IActionResult> UpdateProductPrice(UpdateProductPriceCommand request, CancellationToken cancellationToken)
    {
        Result<bool> result = await Sender.Send(request, cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProductById(Guid productId, CancellationToken cancellationToken)
    {
        Result<Product> result = await Sender.Send(new GetProductByIdQuery(productId), cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("by-sku")]
    public async Task<IActionResult> GetProductBySku(string SKU, CancellationToken cancellationToken)
    {
        Result<Product> result = await Sender.Send(new GetProductBySkuQuery(SKU), cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("by-category")]
    public async Task<IActionResult> GetProductsByCategoryId(Guid categoryId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken)
    {
        Result<CustomList<Product>> result = await Sender.Send(new GetProductsByCategoryIdQuery(categoryId, fromDate, toDate), cancellationToken);
        return CustomResult(result);
    }

    [HttpGet("by-stock-quantity")]
    public async Task<IActionResult> GetProductsByStockQuantity(int quantity, CancellationToken cancellationToken)
    {
        Result<CustomList<Product>> result = await Sender.Send(new GetProductsByStockQuantityQuery(quantity), cancellationToken);
        return CustomResult(result);
    }
}
