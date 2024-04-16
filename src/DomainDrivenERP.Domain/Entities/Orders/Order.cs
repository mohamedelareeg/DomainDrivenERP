using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.LineItems;
using DomainDrivenERP.Domain.Entities.Orders.DomainEvents;
using DomainDrivenERP.Domain.Entities.Products;
using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Errors;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;
using DomainDrivenERP.Domain.Shared.Results;
using DomainDrivenERP.Domain.ValueObjects;

namespace DomainDrivenERP.Domain.Entities.Orders;
public class Order : AggregateRoot, IAuditableEntity
{
    private readonly List<LineItem> _lineItems;

    public Order() { }

    private Order(Guid id, Guid customerId)
        : base(id)
    {
        Guard.Against.Null(id, nameof(id));
        Guard.Against.Null(customerId, nameof(customerId));

        CustomerId = customerId;
        Status = OrderStatus.Created;
    }

    public Guid CustomerId { get; private set; }
    public IReadOnlyCollection<LineItem> LineItems => _lineItems.AsReadOnly();
    public OrderStatus Status { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Result<Order> Create(Guid customerId)
    {
        var id = Guid.NewGuid();
        if (customerId == Guid.Empty)
        {
            return Result.Failure<Order>(DomainErrors.OrderErrors.InvalidCustomerId);
        }

        var order = new Order(id, customerId);
        order.RaiseDomainEvent(new CreateOrderDomainEvent(id, customerId));
        return Result.Success(order);
    }
    public Result<Order> AddLineItem(Guid productId, decimal productPrice, int quantity)
    {
        Guard.Against.Null(productId, nameof(productId));
        Guard.Against.NumberNegativeOrZero(quantity, nameof(quantity));
        Guard.Against.NumberNegativeOrZero(productPrice, nameof(productPrice));
        Result<Price> productPriceResult = Price.Create(productPrice, "USD");
        if (productPriceResult.IsFailure)
        {
            return Result.Failure<Order>(productPriceResult.Error);
        }
        var lineItem = new LineItem(Guid.NewGuid(), Id, productId, quantity, productPriceResult.Value);

        _lineItems.Add(lineItem);
        RaiseDomainEvent(new AddLineItemDomainEvent(Id, lineItem.Id));
        return Result.Success(this);
    }
    public Result<Order> RemoveLineItem(Guid lineItemId)
    {
        LineItem lineItemToRemove = _lineItems.Find(li => li.Id == lineItemId);
        if (lineItemToRemove == null)
        {
            return Result.Failure<Order>(DomainErrors.OrderErrors.LineItemNotFound);
        }

        _lineItems.Remove(lineItemToRemove);
        RaiseDomainEvent(new RemoveLineItemDomainEvent(Id, lineItemId));
        return Result.Success(this);
    }

    public Result<Order> ChangeStatus(OrderStatus newStatus)
    {
        switch (newStatus)
        {
            case OrderStatus.Processing when Status == OrderStatus.Created:
            case OrderStatus.Shipped when Status == OrderStatus.Processing:
            case OrderStatus.Delivered when Status == OrderStatus.Shipped:
                {
                    Status = newStatus;
                    RaiseDomainEvent(new ChangeOrderStatusDomainEvent(Id, newStatus));
                    return Result.Success(this);
                }
            default:
                return Result.Failure<Order>(DomainErrors.OrderErrors.InvalidStatusTransition);
        }
    }
    public Result<Order> CancelOrder()
    {
        if (Status != OrderStatus.Created && Status != OrderStatus.Processing)
        {
            return Result.Failure<Order>(DomainErrors.OrderErrors.InvalidStatusForCancellation);
        }

        Status = OrderStatus.Cancelled;
        RaiseDomainEvent(new CancelOrderDomainEvent(Id));
        return Result.Success(this);
    }
}
