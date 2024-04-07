using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Journals;

namespace CleanArchitectureWithDDD.Application.Features.Journals.Commands.CreateJournal;
public class CreateJournalCommand : ICommand<Journal>
{
    public CreateJournalCommand(DateTime journalDate, string journalDescription, List<TransactionDto> transactions)
    {
        JournalDate = journalDate;
        JournalDescription = journalDescription;
        Transactions = transactions;
    }

    public DateTime JournalDate { get; }
    public string JournalDescription { get; }
    public List<TransactionDto> Transactions { get; }

}
