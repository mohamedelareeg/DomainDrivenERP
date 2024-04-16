using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaWithChildrens;
public class GetCoaWithChildrensQueryValidator : AbstractValidator<GetCoaWithChildrensQuery>
{
    public GetCoaWithChildrensQueryValidator()
    {
        RuleFor(a => a.HeadCode).NotEmpty().WithMessage("Head Code can't be Empty");
    }
}
