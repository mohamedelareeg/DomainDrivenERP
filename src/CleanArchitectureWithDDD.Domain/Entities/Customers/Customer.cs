using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Errors;
using CleanArchitectureWithDDD.Domain.Exceptions;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers;

public sealed class Customer : AggregateRoot, IAuditableEntity
{
    private readonly List<Invoice> _invoices = new();
    private Customer() { }
    private Customer(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email,
        string phone)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
    }

    public static Customer Create(//Factory Method
      Guid id,
      FirstName firstName,
      LastName lastName,
      Email email,
      string phone)
    {

        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(Guid.NewGuid(), customer.Id));
        return customer;
    }

    //Completeness & Performance
    public static async Task<Result<Customer>> Create(//Domain Model Completeness & Domain Model Performance & Losing Domain Model Purity
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email,
        string phone,
        ICustomerRespository customerRespository)
    {
        if (!await customerRespository.IsEmailUniqueAsync(email))
        {
            return Result.Failure<Customer>(new Error("Customer.CreateCustomer", "Email Already Exist"));
        }

        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(Guid.NewGuid(), customer.Id));
        return customer;
    }

    //Completeness & Purity
    public static async Task<Result<Customer>> Create(//Domain Model Completeness & Domain Model Purity & Losing Domain Model Performance
       Guid id,
       FirstName firstName,
       LastName lastName,
       Email email,
       string phone,
       Customer[] customers)
    {
        if (customers.Any(m => m.Email == email))
        {
            return Result.Failure<Customer>(new Error("Customer.CreateCustomer", "Email Already Exist"));
        }
        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(Guid.NewGuid(), customer.Id));
        return customer;

    }

    //Completeness & Purity & Performance Are Achieved
    public static Result<Customer> Create(//Domain Model Completeness & Domain Model Purity & Losing Domain Model Performance
       Guid id,
       FirstName firstName,
       LastName lastName,
       Email email,
       string phone,
       bool isEmailUnique)
    {
        if (!isEmailUnique)
        {
            return Result.Failure<Customer>(DomainErrors.CustomerErrors.IsCustomerEmailAlreadyExist);
        }
        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(Guid.NewGuid(), customer.Id));
        return customer;

    }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public string Phone { get; private set; }
    public IReadOnlyCollection<Invoice> Invoices => _invoices;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }
    public Result<Invoice> CreateInvoice(
        string invoiceSerial,
        DateTime invoiceDate,
        decimal invoiceAmount,
        decimal taxRate = 0.14m,
        decimal discountRate = 0.10m)
    {
        if (this == null)
        {
            // Way 1 :
            // Advantage: Custom Result
            // Custom result objects can provide structured error handling and additional data.
            // They allow developers to convey both success and failure outcomes in a unified way.
            // Disadvantage: No Stack Trace
            // Custom result objects typically don't include stack trace information, which can be useful for debugging.
            return Result.Failure<Invoice>(DomainErrors.CustomerErrors.IsNulledCustomer);

            // Way 2 :
            // Advantage: Custom Exception
            // Custom exceptions can provide detailed error information and include stack trace.
            // They can be caught and handled at various levels in the application.
            // Disadvantage: Lower Performance
            // Throwing and handling exceptions can have a performance overhead compared to returning custom result objects.
            // They are typically used for exceptional circumstances and not for regular control flow.
            throw new CreateInvoiceOfCustomerIsNullDomainException("Cannot create invoice for null customer.");
        }

        decimal invoiceTax = invoiceAmount * taxRate;
        decimal invoiceDiscount = invoiceAmount * discountRate;
        decimal invoiceTotal = invoiceAmount + invoiceTax - invoiceDiscount;

        var invoice = new Invoice(Guid.NewGuid(), invoiceSerial, invoiceDate, invoiceAmount, invoiceDiscount, invoiceTax, invoiceTotal, Id);
        _invoices.Add(invoice);
        RaiseDomainEvent(new CreateInvoiceDomainEvent(Guid.NewGuid(), Id, invoice));
        return invoice;
    }
    public Result<Invoice> UpdateCustomerInvoiceStatus(Invoice invoice, InvoiceStatus newStatus)
    {
        Invoice? invoiceToUpdate = _invoices.FirstOrDefault(i => i.Id == invoice.Id);
        if (invoiceToUpdate == null)
        {
            return Result.Failure<Invoice>(new Error("Invoice.NotFound", "Invoice not found."));
        }

        invoiceToUpdate.UpdateInvoiceStatus(newStatus);
        RaiseDomainEvent(new UpdateInvoiceStatusDomainEvent(Guid.NewGuid(), Id, invoice, newStatus));
        return invoiceToUpdate;
    }
}
