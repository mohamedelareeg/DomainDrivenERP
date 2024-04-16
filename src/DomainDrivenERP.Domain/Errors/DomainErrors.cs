using DomainDrivenERP.Domain.Shared.Results;

namespace DomainDrivenERP.Domain.Errors;

public static class DomainErrors
{
    public static class CustomerErrors
    {
        public static readonly Error IsNulledCustomer = new Error("Customers.CreateInvoice", "Cannot create invoice for null customer.");
        public static readonly Error IsCustomerEmailAlreadyExist = new Error("Customer.CreateCustomer", "Email Already Exist");
    }
    public static class EmailErrors
    {
        public static readonly Error Empty = new Error("Email.Empty", "Email is Empty");
        public static readonly Error TooLong = new Error("Email.TooLong", "Email is Too Long");
        public static readonly Error NotValid = new Error("Email.InvalidFormat", "Email is not in a valid format");
    }
    public static class CategoryErrors
    {
        public static readonly Error InvalidCategoryId = new Error("Category.InvalidId", "Invalid category ID.");
        public static readonly Error InvalidCategoryName = new Error("Category.InvalidName", "Invalid category name.");
        public static readonly Error ProductNotFound = new Error("Category.ProductNotFound", "Product not found in category.");
    }
    public static class ProductErrors
    {
        public static readonly Error InvalidProductName = new Error("Product.InvalidName", "Invalid product name.");
        public static readonly Error InvalidProductPrice = new Error("Product.InvalidPrice", "Invalid product price.");
        public static readonly Error InvalidStockQuantity = new Error("Product.InvalidStockQuantity", "Invalid stock quantity.");
        public static readonly Error InvalidBarcode = new Error("Product.InvalidBarcode", "Invalid product barcode.");
        public static readonly Error InvalidModel = new Error("Product.InvalidModel", "Invalid product model.");
        public static readonly Error InvalidDetails = new Error("Product.InvalidDetails", "Invalid product details.");
        public static readonly Error DuplicateProductName = new Error("Product.DuplicateName", "Product with the same name already exists.");
        public static readonly Error ProductNotFound = new Error("Product.NotFound", "Product not found.");
        public static readonly Error InvalidDiscountPercentage = new Error("Product.InvalidDiscountPercentage", "Invalid discount percentage.");
        public static readonly Error InvalidCategoryId = new Error("Product.InvalidCategoryId", "Invalid category ID.");
        public static readonly Error InsufficientStock = new Error("Product.InsufficientStock", "Insufficient stock quantity.");


    }
    public static class PriceErrors
    {
        public static readonly Error InvalidCurrency = new Error("Price.InvalidCurrency", "Currency must not be empty or null.");
        public static readonly Error InvalidAmount = new Error("Price.InvalidAmount", "Amount must be greater than zero.");
    }
    public static class SkuErrors
    {
        public static readonly Error InvalidSKU = new Error("Sku.InvalidSKU", "The SKU must not be null or empty.");
        public static readonly Error InvalidSKUFormat = new Error("Sku.invalid_sku_format", "Invalid SKU format.");
        public static readonly Error InvalidSKULength = new Error("Sku.invalid_sku_length", "Invalid SKU length.");
    }
    public static class OrderErrors
    {
        public static readonly Error InvalidOrderId = new Error("Order.InvalidOrderId", "The order ID is invalid.");
        public static readonly Error InvalidCustomerId = new Error("Order.InvalidCustomerId", "The customer ID is invalid.");
        public static readonly Error NoLineItems = new Error("Order.NoLineItems", "The order must contain at least one line item.");
        public static readonly Error LineItemNotFound = new Error("Order.LineItemNotFound", "The specified line item was not found in the order.");
        public static readonly Error InvalidLineItemId = new Error("Order.InvalidLineItemId", "The line item ID is invalid.");
        public static readonly Error InvalidProductId = new Error("Order.InvalidProductId", "The product ID is invalid.");
        public static readonly Error InvalidQuantity = new Error("Order.InvalidQuantity", "The quantity must be greater than zero.");
        public static readonly Error InvalidUnitPrice = new Error("Order.InvalidUnitPrice", "The unit price must be greater than zero.");
        public static readonly Error InvalidStatusTransition = new Error("Order.InvalidStatusTransition", "Invalid status transition for the order.");
        public static readonly Error InvalidStatusForCancellation = new Error("Order.InvalidStatusForCancellation", "The order cannot be cancelled because it is not in a cancellable status.");

    }
}
