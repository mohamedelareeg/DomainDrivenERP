using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Dtos;
using FluentValidation;

namespace CleanArchitectureWithDDD.Application.Features.Journals.Commands.CreateJournal;
public class TransactionDtoValidator : AbstractValidator<TransactionDto>
{
    public TransactionDtoValidator()
    {
        RuleFor(transaction => transaction.Debit)
          .Must((transaction, debit) => debit != 0 || transaction.Credit != 0)
          .WithMessage("Each transaction must have either a debit or a credit, not both or neither.");

        RuleFor(transaction => transaction.Credit)
            .Must((transaction, credit) => credit != 0 || transaction.Debit != 0)
            .WithMessage("Each transaction must have either a debit or a credit, not both or neither.");

    }
}
