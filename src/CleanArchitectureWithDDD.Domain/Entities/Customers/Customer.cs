using CleanArchitectureWithDDD.Domain.Abstractions.Persistence.Repositories;
using CleanArchitectureWithDDD.Domain.Entities.Customers.DomainEvents;
using CleanArchitectureWithDDD.Domain.Entities.Invoices;
using CleanArchitectureWithDDD.Domain.Entities.Orders;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Errors;
using CleanArchitectureWithDDD.Domain.Exceptions;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared.Guards;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using CleanArchitectureWithDDD.Domain.ValueObjects;

namespace CleanArchitectureWithDDD.Domain.Entities.Customers;

public sealed class Customer : AggregateRoot, IAuditableEntity
{
    private readonly List<Invoice> _invoices = new();
    private readonly List<Order> _orders = new();

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

    public static Customer Create(// Factory Method
      FirstName firstName,
      LastName lastName,
      Email email,
      string phone)
    {
        Guard.Against.NullOrEmpty(firstName.Value, nameof(firstName));
        Guard.Against.NullOrEmpty(lastName.Value, nameof(lastName));
        Guard.Against.NullOrEmpty(email.Value, nameof(email));
        Guard.Against.NullOrEmpty(phone, nameof(phone));

        var id = Guid.NewGuid();
        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(customer.Id));
        return customer;
    }

    // Completeness & Performance
    public static async Task<Result<Customer>> Create(// Domain Model Completeness & Domain Model Performance & Losing Domain Model Purity
        FirstName firstName,
        LastName lastName,
        Email email,
        string phone,
        ICustomerRespository customerRespository)
    {

        Guard.Against.NullOrEmpty(firstName.Value, nameof(firstName));
        Guard.Against.NullOrEmpty(lastName.Value, nameof(lastName));
        Guard.Against.NullOrEmpty(email.Value, nameof(email));
        Guard.Against.NullOrEmpty(phone, nameof(phone));
        Guard.Against.Null(customerRespository, nameof(customerRespository));

        var id = Guid.NewGuid();

        if (!await customerRespository.IsEmailUniqueAsync(email))
        {
            return Result.Failure<Customer>(new Error("Customer.CreateCustomer", "Email Already Exist"));
        }

        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(customer.Id));
        return customer;
    }

    // Completeness & Purity
    public static async Task<Result<Customer>> Create(// Domain Model Completeness & Domain Model Purity & Losing Domain Model Performance
       FirstName firstName,
       LastName lastName,
       Email email,
       string phone,
       Customer[] customers)
    {
        Guard.Against.NullOrEmpty(firstName.Value, nameof(firstName));
        Guard.Against.NullOrEmpty(lastName.Value, nameof(lastName));
        Guard.Against.NullOrEmpty(email.Value, nameof(email));
        Guard.Against.NullOrEmpty(phone, nameof(phone));
        Guard.Against.Null(customers, nameof(customers));

        var id = Guid.NewGuid();

        if (customers.Any(m => m.Email == email))
        {
            return Result.Failure<Customer>(new Error("Customer.CreateCustomer", "Email Already Exist"));
        }
        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(customer.Id));
        return customer;

    }

    // Completeness & Purity & Performance Are Achieved
    public static Result<Customer> Create(// Domain Model Completeness & Domain Model Purity & Losing Domain Model Performance
       FirstName firstName,
       LastName lastName,
       Email email,
       string phone,
       bool isEmailUnique)
    {
        Guard.Against.NullOrEmpty(firstName.Value, nameof(firstName));
        Guard.Against.NullOrEmpty(lastName.Value, nameof(lastName));
        Guard.Against.NullOrEmpty(email.Value, nameof(email));
        Guard.Against.NullOrEmpty(phone, nameof(phone));

        var id = Guid.NewGuid();

        if (!isEmailUnique)
        {
            return Result.Failure<Customer>(DomainErrors.CustomerErrors.IsCustomerEmailAlreadyExist);
        }
        var customer = new Customer(id, firstName, lastName, email, phone);
        customer.RaiseDomainEvent(new CreateCustomerDomainEvent(customer.Id));
        return customer;

    }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public string Phone { get; private set; }
    public IReadOnlyCollection<Invoice> Invoices => _invoices;
    public IReadOnlyCollection<Order> Orders => _orders;

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }
    public Result<Invoice> CreateInvoice(
        string invoiceSerial,
        DateTime invoiceDate,
        decimal invoiceAmount,
        decimal taxRate = 0.14m,
        decimal discountRate = 0.10m)
    {
        Guard.Against.NullOrEmpty(invoiceSerial, nameof(invoiceSerial));
        Guard.Against.NullOrEmpty(invoiceDate.ToString(), nameof(invoiceDate));
        Guard.Against.NumberZero(invoiceAmount, nameof(invoiceAmount));

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
        RaiseDomainEvent(new CreateInvoiceDomainEvent(Id, invoice));
        return invoice;
    }
    public void AddInvoice(params Invoice[] invoices)
    {
        Guard.Against.Null(invoices, nameof(invoices));

        if (invoices == null || invoices.Length == 0)
        {
            throw new ArgumentException("At least one invoice must be provided.", nameof(invoices));
        }

        foreach (Invoice invoice in invoices)
        {
            if (invoice.CustomerId != Id)
            {
                throw new ArgumentException("The provided invoice does not belong to this customer.");
            }

            if (_invoices.Any(i => i.Id == invoice.Id))
            {
                throw new InvalidOperationException($"Invoice with ID '{invoice.Id}' has already been added to this customer.");
            }

            _invoices.Add(invoice);
        }
    }

    public Result<Invoice> UpdateCustomerInvoiceStatus(Invoice invoice, InvoiceStatus newStatus)
    {
        Guard.Against.Null(invoice, nameof(invoice));
        Guard.Against.Null(newStatus, nameof(newStatus));

        Invoice? invoiceToUpdate = _invoices.FirstOrDefault(i => i.Id == invoice.Id);
        if (invoiceToUpdate == null)
        {
            return Result.Failure<Invoice>(new Error("Invoice.NotFound", "Invoice not found."));
        }

        invoiceToUpdate.UpdateInvoiceStatus(newStatus);
        RaiseDomainEvent(new UpdateInvoiceStatusDomainEvent(Id, invoice, newStatus));
        return invoiceToUpdate;
    }
    public CustomerSnapshot ToSnapShot()
    {
        return new CustomerSnapshot
        {
            Id = Id,
            Email = Email.Value,
            FirstName = FirstName.Value,
            LastName = LastName.Value,
            Phone = Phone,
            CreatedOnUtc = CreatedOnUtc,
            ModifiedOnUtc = ModifiedOnUtc,
        };
    }
    public static Customer FromSnapshot(CustomerSnapshot snapshot)
    {
        return new Customer(
            snapshot.Id,
            FirstName.Create(snapshot.FirstName).Value,
            LastName.Create(snapshot.LastName).Value,
            Email.Create(snapshot.Email).Value,
            snapshot.Phone)
        {
            CreatedOnUtc = snapshot.CreatedOnUtc,
            ModifiedOnUtc = snapshot.ModifiedOnUtc
        };
    }

}
