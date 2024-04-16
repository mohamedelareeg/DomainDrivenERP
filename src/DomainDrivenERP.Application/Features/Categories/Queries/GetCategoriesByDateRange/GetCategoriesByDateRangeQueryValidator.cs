using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Categories.Queries.GetCategoriesByDateRange;
public class GetCategoriesByDateRangeQueryValidator : AbstractValidator<GetCategoriesByDateRangeQuery>
{
    public GetCategoriesByDateRangeQueryValidator()
    {
        RuleFor(query => query.FromDate).NotEmpty().WithMessage("From date must not be empty.");
        RuleFor(query => query.ToDate).NotEmpty().WithMessage("To date must not be empty.")
                                      .GreaterThan(query => query.FromDate).WithMessage("To date must be greater than from date.");
    }
}
