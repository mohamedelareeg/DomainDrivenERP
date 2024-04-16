using System;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Errors;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Domain.Entities.LineItems;

public class LineItem : BaseEntity
{
    public LineItem()
    {

    }
    internal LineItem(Guid id, Guid orderId, Guid productId, int quantity, Price unitPrice)
        : base(id)
    {
        Guard.Against.Null(id, nameof(id));
        Guard.Against.Null(orderId, nameof(orderId));
        Guard.Against.Null(productId, nameof(productId));
        Guard.Against.NumberNegativeOrZero(quantity, nameof(quantity));
        Guard.Against.Null(unitPrice, nameof(unitPrice));

        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Price UnitPrice { get; private set; }

    public decimal TotalPrice => Quantity * UnitPrice.Amount;

    public static Result<LineItem> Create(Guid orderId, Guid productId, int quantity, decimal amount, string currency)
    {
        var id = Guid.NewGuid();

        if (orderId == Guid.Empty)
        {
            return Result.Failure<LineItem>(DomainErrors.OrderErrors.InvalidOrderId);
        }

        if (productId == Guid.Empty)
        {
            return Result.Failure<LineItem>(DomainErrors.OrderErrors.InvalidProductId);
        }

        if (quantity <= 0)
        {
            return Result.Failure<LineItem>(DomainErrors.OrderErrors.InvalidQuantity);
        }

        Result<Price> priceResult = Price.Create(amount, currency);
        if (priceResult.IsFailure)
        {
            return Result.Failure<LineItem>(priceResult.Error);
        }


        var lineItem = new LineItem(id, orderId, productId, quantity, priceResult.Value);
        return Result.Success(lineItem);
    }
}
