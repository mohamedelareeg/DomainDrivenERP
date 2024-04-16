using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Categories.Commands.UpdateCategoryName;
public class UpdateCategoryNameCommandValidator : AbstractValidator<UpdateCategoryNameCommand>
{
    public UpdateCategoryNameCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.NewName).NotEmpty().MaximumLength(100);
    }
}
