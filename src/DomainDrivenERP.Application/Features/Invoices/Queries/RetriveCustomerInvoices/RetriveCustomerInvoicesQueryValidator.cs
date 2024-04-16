using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Invoices.Queries.RetriveCustomerInvoice;
internal class RetriveCustomerInvoicesQueryValidator : AbstractValidator<RetriveCustomerInvoicesQuery>
{
    public RetriveCustomerInvoicesQueryValidator()
    {
        RuleFor(a => a.CustomerId).NotEmpty().WithMessage("Customer Id can't be Empty");
    }
}
