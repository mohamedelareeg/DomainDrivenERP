using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductsByStockQuantity;
public class GetProductsByStockQuantityQueryValidator : AbstractValidator<GetProductsByStockQuantityQuery>
{
    public GetProductsByStockQuantityQueryValidator()
    {
        RuleFor(query => query.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}
