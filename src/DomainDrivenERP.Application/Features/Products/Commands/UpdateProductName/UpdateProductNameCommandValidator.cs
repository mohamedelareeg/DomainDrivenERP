using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Products.Commands.UpdateProductName;
public class UpdateProductNameCommandValidator : AbstractValidator<UpdateProductNameCommand>
{
    public UpdateProductNameCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.NewName).NotEmpty().MaximumLength(100);
    }
}
