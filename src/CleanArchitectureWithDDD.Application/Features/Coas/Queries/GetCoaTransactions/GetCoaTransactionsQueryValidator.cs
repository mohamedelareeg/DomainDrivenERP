using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaTransactions;
internal class GetCoaTransactionsQueryValidator : AbstractValidator<GetCoaTransactionsQuery>
{
    public GetCoaTransactionsQueryValidator()
    {
        RuleFor(x => x.AccountName).NotEmpty().When(x => string.IsNullOrEmpty(x.AccountHeadCode))
            .WithMessage("AccountName cannot be empty when AccountHeadCode is empty.");
        RuleFor(x => x.AccountHeadCode).NotEmpty().When(x => string.IsNullOrEmpty(x.AccountName))
            .WithMessage("AccountHeadCode cannot be empty when AccountName is empty.");
    }
}
