using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Entities.COAs;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Domain.Primitives;
using DomainDrivenERP.Domain.Shared.Guards;

namespace DomainDrivenERP.Domain.Entities.Transactions;
public sealed class Transaction : BaseEntity
{
    internal Transaction()
    {

    }
    internal Transaction(Guid transactionId, Guid journalId, string cOAId, double debit, double credit)
    {
        Guard.Against.NullOrWhiteSpace(transactionId.ToString(), nameof(transactionId));
        Guard.Against.NullOrWhiteSpace(journalId.ToString(), nameof(journalId));
        Guard.Against.NullOrWhiteSpace(cOAId, nameof(cOAId));
        Guard.Against.NumberNegativeOrZero(debit, nameof(debit));
        Guard.Against.NumberNegativeOrZero(credit, nameof(credit));

        TransactionId = transactionId;
        JournalId = journalId;
        COAId = cOAId;
        Debit = debit;
        Credit = credit;
    }

    public Guid TransactionId { get; private set; }
    public Guid JournalId { get; private set; }
    public Journal Journal { get; private set; }
    public string COAId { get; private set; }
    public COA COA { get; private set; }
    public double Debit { get; private set; }
    public double Credit { get; private set; }

    public TransactionSnapshot ToSnapshot()
    {
        return new TransactionSnapshot
        {
            TransactionId = TransactionId,
            JournalId = JournalId,
            COAId = COAId,
            Debit = Debit,
            Credit = Credit
        };
    }
    public static Transaction FromSnapshot(TransactionSnapshot snapshot)
    {
        return new Transaction(
            snapshot.TransactionId,
            snapshot.JournalId,
            snapshot.COAId,
            snapshot.Debit,
            snapshot.Credit
        );
    }

}
