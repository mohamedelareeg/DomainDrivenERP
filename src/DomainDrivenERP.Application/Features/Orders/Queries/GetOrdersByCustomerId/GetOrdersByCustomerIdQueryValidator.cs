using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Orders.Queries.GetOrdersByCustomerId;
public class GetOrdersByCustomerIdQueryValidator : AbstractValidator<GetOrdersByCustomerIdQuery>
{
    public GetOrdersByCustomerIdQueryValidator()
    {
        RuleFor(query => query.CustomerId).NotEmpty().WithMessage("Customer ID must not be empty.");
        RuleFor(query => query.FromDate).NotEmpty().WithMessage("From date must not be empty.");
        RuleFor(query => query.ToDate).NotEmpty().WithMessage("To date must not be empty.")
            .GreaterThanOrEqualTo(query => query.FromDate).WithMessage("To date must be greater than or equal to From date.");
    }
}
