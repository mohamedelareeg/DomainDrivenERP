using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Dtos;
using FluentValidation;

namespace DomainDrivenERP.Application.Features.Journals.Commands.CreateJournal;
public class CreateJournalCommandValidator : AbstractValidator<CreateJournalCommand>
{
    public CreateJournalCommandValidator()
    {
        RuleFor(command => command.JournalDescription)
          .NotEmpty().WithMessage("Description must not be empty.");

        RuleFor(command => command.JournalDate)
            .NotEmpty().WithMessage("Journal date must not be empty.")
            .Must(date => date <= DateTime.Today).WithMessage("Journal date must not exceed today's date.");

        RuleFor(command => command.Transactions)
            .NotEmpty().WithMessage("Transactions list must not be empty.")
            .Must(transactions => transactions != null && transactions.Count >= 2)
            .WithMessage("At least 2 transactions are required.");

        RuleForEach(command => command.Transactions)
            .SetValidator(new TransactionDtoValidator());

        RuleFor(command => command.Transactions)
          .Must(BeUniqueAccountNames)
          .WithMessage("Account names or account head codes must be unique.");

        RuleFor(command => command.Transactions.Sum(transaction => transaction.Debit))
            .Equal(command => command.Transactions.Sum(transaction => transaction.Credit))
            .WithMessage("The sum of debits must be equal to the sum of credits.");
    }
    public static bool BeUniqueAccountNames(List<TransactionDto> transactionsList)
    {
        for (int i = 0; i < transactionsList.Count; i++)
        {
            for (int j = i + 1; j < transactionsList.Count; j++)
            {
                TransactionDto transaction1 = transactionsList[i];
                TransactionDto transaction2 = transactionsList[j];

                string?[] accountNames = new[] { transaction1.AccountName, transaction2.AccountName };
                string?[] accountHeadCodes = new[] { transaction1.AccountHeadCode, transaction2.AccountHeadCode };

                if (accountNames.All(x => x != null) && accountNames.Distinct().Count() != accountNames.Length)
                {
                    return false;
                }

                if (accountHeadCodes.All(x => x != null) && accountHeadCodes.Distinct().Count() != accountHeadCodes.Length)
                {
                    return false;
                }
            }
        }

        return true;
    }
}
