using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Commands.CreateCoa;
public class CreateCoaCommandValidator :AbstractValidator<CreateCoaCommand>
{
    public CreateCoaCommandValidator()
    {
        RuleFor(x => x.CoaName).NotEmpty().WithMessage("Coa Head Name Is Empty");
        RuleFor(x => x.CoaParentName).NotEmpty().WithMessage("Coa Parent Head Name Is Empty");
    }
}
