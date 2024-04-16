using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Invoices.Commands.CreateCustomerInvoice;
internal class CreateCustomerInvoiceCommandValidator : AbstractValidator<CreateCustomerInvoiceCommand>
{
    public CreateCustomerInvoiceCommandValidator()
    {
        RuleFor(a => a.CustomerId).NotEmpty().WithMessage("Customer Id must not be empty.");
        RuleFor(a => a.InvoiceSerial).NotEmpty().WithMessage("Invoice Serial must not be empty.");
        RuleFor(a => a.InvoiceDate)
          .NotEmpty().WithMessage("Invoice date must not be empty.")
          .Must(a => a <= DateTime.Today).WithMessage("Invoice date must not exceed today's date.");
        RuleFor(a => a.InvoiceAmount).NotEmpty().WithMessage("Invoice Amount must not be empty.")
            .Must(a => a > 0).WithMessage("Invoice Amount can't be less than 1.");
    }
}
