using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Commands.CreateFirstLevelCoa;
public class CreateFirstLevelCoaCommandValidator : AbstractValidator<CreateFirstLevelCoaCommand>
{
    public CreateFirstLevelCoaCommandValidator()
    {
        RuleFor(a => a.HeadName).NotEmpty().WithMessage("Head Name can't be null or empty");
        RuleFor(a => a.Type).NotNull().WithMessage("Please Provide Type for the COA");
    }
}
