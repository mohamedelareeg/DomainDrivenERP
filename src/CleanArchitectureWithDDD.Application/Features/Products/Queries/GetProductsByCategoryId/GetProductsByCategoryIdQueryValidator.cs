using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Products.Queries.GetProductsByCategoryId;
public class GetProductsByCategoryIdQueryValidator : AbstractValidator<GetProductsByCategoryIdQuery>
{
    public GetProductsByCategoryIdQueryValidator()
    {
        RuleFor(query => query.CategoryId).NotEmpty().WithMessage("Category ID must not be empty.");
        RuleFor(query => query.FromDate).NotEmpty().WithMessage("From date must not be empty.");
        RuleFor(query => query.ToDate).NotEmpty().WithMessage("To date must not be empty.");
    }
}
