using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Errors;
using CleanArchitectureWithDDD.Domain.Exceptions;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;
using CleanArchitectureWithDDD.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.Entities
{
    public sealed class Customer : AggregateRoot
    {
        private readonly List<Invoice> _invoices = new();
        public Customer(
            Guid id,
            FirstName firstName,
            LastName lastName,
            string email,
            string phone) : base(id) { 
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        public FirstName FirstName { get; private set; }
        public LastName LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public IReadOnlyCollection<Invoice> Invoices => _invoices;
        public Result<Invoice> CreateInvoice(
            string invoiceId,
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
                return Result.Failure<Invoice>(DomainErrors.Customers.IsNulledCustomer);

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

            var invoice = new Invoice(Guid.NewGuid(), invoiceId, invoiceDate, invoiceAmount, invoiceDiscount, invoiceTax, invoiceTotal);
            _invoices.Add(invoice);
            return invoice;
        }
        public Result UpdateCustomerInvoiceStatus(Invoice invoice, InvoiceStatus newStatus)
        {
            var invoiceToUpdate = _invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if (invoiceToUpdate == null)
            {
                return Result.Failure(new Error("Invoice.NotFound", "Invoice not found."));
            }
            invoiceToUpdate.UpdateInvoiceStatus(newStatus);
            return Result.Success();
        }
    }
}
