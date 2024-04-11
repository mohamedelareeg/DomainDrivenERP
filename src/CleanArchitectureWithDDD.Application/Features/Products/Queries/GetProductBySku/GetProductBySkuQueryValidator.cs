using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductBySku;
public class GetProductBySkuQueryValidator : AbstractValidator<GetProductBySkuQuery>
{
    public GetProductBySkuQueryValidator()
    {
        RuleFor(query => query.SKU).NotEmpty().WithMessage("SKU must not be empty.");
    }
}
