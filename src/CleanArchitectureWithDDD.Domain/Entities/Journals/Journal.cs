using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.DomainEvents;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities.Transactions;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Shared;

namespace CleanArchitectureWithDDD.Domain.Entities.Journals;
public sealed class Journal : AggregateRoot
{
    private readonly List<Transaction> _transactions = new();

    public Journal(Guid id, string? description, bool isOpening, DateTime journalDate, JournalStatus status)
        : base(id)
    {
        Id = id;
        Description = description;
        IsOpening = isOpening;
        JournalDate = journalDate;
        Status = status;
    }
    public static Result<Journal> Create(string? description, bool isOpening, DateTime journalDate, List<TransactionDto> transactions)
    {
        if (transactions is null)
        {
            return Result.Failure<Journal>(new Error("Journal.Create", "Transactions list cannot be null."));
        }
        var journal = new Journal(Guid.NewGuid(), description, isOpening, journalDate, JournalStatus.Pending);
        journal.AddTransactions(transactions);

        journal.RaiseDomainEvent(new JournalCreatedDomainEvent(Guid.NewGuid(), journal.Id, description, journalDate));
        return journal;
    }
    public Result UpdateJournalStatus(JournalStatus newStatus)
    {
        if (newStatus == Status)
        {
            return Result.Failure<Journal>(new Error("Journal.UpdateStatus", "New status is the same as the current status."));
        }

        Status = newStatus;
        return Result.Success();
    }
    public void AddTransactions(params Transaction[] transactions)
    {
        if (transactions == null || transactions.Length == 0)
        {
            throw new ArgumentException("At least one transaction must be provided.", nameof(transactions));
        }

        foreach (Transactions.Transaction transaction in transactions)
        {
            if (transaction.JournalId != Id)
            {
                throw new ArgumentException("The provided transaction does not belong to this journal.");
            }
            _transactions.Add(transaction);
        }
    }
    public Result AddTransactions(List<TransactionDto> transactions)
    {
        if (transactions is null)
        {
            return Result.Failure<Journal>(new Error("Journal.AddTransactions", "Transactions list cannot be null."));
        }
        foreach (TransactionDto dto in transactions)
        {
            var transaction = new Transaction(
                transactionId: Guid.NewGuid(),
                journalId: Id,
                cOAId: dto.AccountHeadCode,
                debit: dto.Debit,
                credit: dto.Credit);
            _transactions.Add(transaction);
        }
        return Result.Success();
    }

    public Result UpdateTransactions(List<Transaction> updatedTransactions)
    {
        if (updatedTransactions is null)
        {
            return Result.Failure<Journal>(new Error("Journal.UpdateTransactions", "Updated transactions list cannot be null."));
        }

        _transactions.Clear();
        _transactions.AddRange(updatedTransactions);
        return Result.Success();
    }

    public Result RemoveTransaction(Transaction transactionToRemove)
    {
        if (!_transactions.Contains(transactionToRemove))
        {
            return Result.Failure<Journal>(new Error("Journal.RemoveTransaction", "Transaction to remove does not exist."));
        }

        _transactions.Remove(transactionToRemove);
        return Result.Success();
    }

    public Guid Id { get; private set; }
    public string? Description { get; private set; }
    public bool IsOpening { get; private set; }
    public DateTime JournalDate { get; private set; }
    public JournalStatus Status { get; private set; }
    public IReadOnlyCollection<Transactions.Transaction> Transactions => _transactions;

    public JournalSnapshot ToSnapshot()
    {
        var snapshot = new JournalSnapshot
        {
            Id = Id,
            Description = Description,
            IsOpening = IsOpening,
            JournalDate = JournalDate,
            Status = Status
        };
        foreach (Transaction transaction in _transactions)
        {
            snapshot.Transactions.Add(transaction.ToSnapshot());
        }
        return snapshot;
    }
    public static Journal FromSnapshot(JournalSnapshot snapshot)
    {
        var journal = new Journal(
            snapshot.Id,
            snapshot.Description,
            snapshot.IsOpening,
            snapshot.JournalDate,
            snapshot.Status);

        foreach (TransactionSnapshot transactionSnapshot in snapshot.Transactions)
        {
            var transaction = Transaction.FromSnapshot(transactionSnapshot);
            journal.AddTransactions(transaction);
        }

        return journal;
    }

}
