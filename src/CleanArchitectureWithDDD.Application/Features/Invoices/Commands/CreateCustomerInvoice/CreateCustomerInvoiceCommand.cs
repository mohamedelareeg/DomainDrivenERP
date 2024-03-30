using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Features.Invoices.Commands.CreateCustomerInvoice
{
    public class CreateCustomerInvoiceCommand : ICommand<bool>
    {
        public Guid CustomerId { get; }
        public string InvoiceSerial { get; }
        public DateTime InvoiceDate { get; }
        public decimal InvoiceAmount { get; }
        public CreateCustomerInvoiceCommand(Guid customerId, string invoiceSerial, DateTime invoiceDate, decimal invoiceAmount)
        {
            CustomerId = customerId;
            InvoiceSerial = invoiceSerial;
            InvoiceDate = invoiceDate;
            InvoiceAmount = invoiceAmount;
        }
    }
}
