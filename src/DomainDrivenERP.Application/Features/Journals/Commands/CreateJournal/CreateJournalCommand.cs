using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.Journals;

namespace DomainDrivenERP.Application.Features.Journals.Commands.CreateJournal;
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
