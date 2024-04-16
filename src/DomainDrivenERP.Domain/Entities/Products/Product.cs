using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.Products.DomainEvents;
using DomainDrivenERP.Domain.Errors;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Domain.Entities.Products;
public class Product : AggregateRoot, IAuditableEntity
{
    public Product() { }
    private Product(Guid id, string name, Price price, int stockQuantity, SKU sku, string model, string details, Guid categoryId)
               : base(id)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NumberNegativeOrZero(price.Amount, nameof(price));
        Guard.Against.NumberNegative(stockQuantity, nameof(stockQuantity));
        Guard.Against.Null(sku, nameof(sku));
        Guard.Against.NullOrEmpty(model, nameof(model));
        Guard.Against.NullOrEmpty(details, nameof(details));
        Guard.Against.Null(categoryId, nameof(categoryId));

        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        SKU = sku;
        Model = model;
        Details = details;
        CategoryId = categoryId;
    }

    public string Name { get; private set; }
    public Price Price { get; private set; }
    public int StockQuantity { get; private set; }
    public SKU SKU { get; private set; }
    public string Model { get; private set; }
    public string Details { get; private set; }
    public Guid CategoryId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Result<Product> Create(string name, decimal amount, string currency, int stockQuantity, string sku, string model, string details, Guid categoryId)
    {
        var id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidProductName);
        }

        if (amount <= 0)
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidProductPrice);
        }
        Result<Price> priceResult = Price.Create(amount, currency);
        if (priceResult.IsFailure)
        {
            return Result.Failure<Product>(priceResult.Error);
        }


        if (stockQuantity < 0)
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidStockQuantity);
        }

        Result<SKU> skuResult = SKU.Create(sku);
        if (skuResult.IsFailure)
        {
            return Result.Failure<Product>(skuResult.Error);
        }
        if (string.IsNullOrWhiteSpace(model))
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidModel);
        }

        if (string.IsNullOrWhiteSpace(details))
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidDetails);
        }

        if (categoryId == Guid.Empty)
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidCategoryId);
        }

        var product = new Product(id, name, priceResult.Value, stockQuantity, skuResult.Value, model, details, categoryId);
        product.RaiseDomainEvent(new CreateProductDomainEvent(product.Id, name, amount, currency, stockQuantity, sku, model, details, categoryId));
        return Result.Success(product);
    }
    public Result<Product> UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidProductName);
        }

        Name = newName;
        RaiseDomainEvent(new UpdateProductNameDomainEvent(Id, newName));
        return Result.Success(this);
    }

    public Result<Product> UpdatePrice(decimal newPriceAmount, string currency)
    {
        if (newPriceAmount <= 0)
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidProductPrice);
        }

        Result<Price> priceResult = Price.Create(newPriceAmount, currency);
        if (priceResult.IsFailure)
        {
            return Result.Failure<Product>(priceResult.Error);
        }
        Price = priceResult.Value;
        RaiseDomainEvent(new UpdateProductPriceDomainEvent(Id, newPriceAmount, currency));
        return Result.Success(this);
    }
    public bool IsInStock(int quantity = 1)
    {
        return StockQuantity >= quantity;
    }

    public Result<Product> DecreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidStockQuantity);
        }

        if (!IsInStock(quantity))
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InsufficientStock);
        }

        StockQuantity -= quantity;
        return Result.Success(this);
    }

    public Result<Product> IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            return Result.Failure<Product>(DomainErrors.ProductErrors.InvalidStockQuantity);
        }

        StockQuantity += quantity;
        return Result.Success(this);
    }
    public Result<decimal> ApplyDiscount(decimal discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100)
        {
            return Result.Failure<decimal>(DomainErrors.ProductErrors.InvalidDiscountPercentage);
        }

        decimal discountFactor = discountPercentage / 100;
        decimal discountedPrice = Price.Amount - Price.Amount * discountFactor;

        return Result.Success(discountedPrice);
    }
}
