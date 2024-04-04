using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities;
public sealed class Transaction
{
    public Transaction(Guid transactionId, Guid journalId, string cOAId, double debit, double credit)
    {
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

}
