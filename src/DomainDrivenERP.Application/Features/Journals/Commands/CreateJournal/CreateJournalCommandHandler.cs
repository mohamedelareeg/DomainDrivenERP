﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Abstractions.Persistence.Data;
using DomainDrivenERP.Domain.Abstractions.Persistence.Repositories;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.Journals;
using DomainDrivenERP.Domain.Shared.Results;
using FluentValidation.Results;

namespace DomainDrivenERP.Application.Features.Journals.Commands.CreateJournal;
internal class CreateJournalCommandHandler : ICommandHandler<CreateJournalCommand, Journal>
{
    private readonly IJournalRepository _journalRepository;
    private readonly ICoaRepository _coaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateJournalCommandHandler(IJournalRepository journalRepository, ICoaRepository coaRepository, IUnitOfWork unitOfWork)
    {
        _journalRepository = journalRepository;
        _coaRepository = coaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Journal>> Handle(CreateJournalCommand request, CancellationToken cancellationToken)
    {
        foreach (TransactionDto item in request.Transactions)
        {
            if (!string.IsNullOrEmpty(item.AccountName))
            {
                string? coaId = await _coaRepository.GetByAccountName(item.AccountName);
                if (string.IsNullOrEmpty(coaId))
                {
                    return Result.Failure<Journal>(new Error("Journal.CreateJournal", $"Account Name with name '{item.AccountName}' does not exist."));
                }
                item.AccountHeadCode = coaId;
                continue;
            }

            if (!string.IsNullOrEmpty(item.AccountHeadCode))
            {
                string? coaId = await _coaRepository.GetByAccountHeadCode(item.AccountHeadCode);
                if (string.IsNullOrEmpty(coaId))
                {
                    return Result.Failure<Journal>(new Error("Journal.CreateJournal", $"Account HeadCode with code '{item.AccountHeadCode}' does not exist."));
                }
            }
        }
        Result<Journal> journalResult = Journal.Create(request.JournalDescription, false, request.JournalDate, request.Transactions);
        if (journalResult.IsFailure)
        {
            return Result.Failure<Journal>(journalResult.Error);
        }
        await _journalRepository.CreateJournal(journalResult.Value);
        await _unitOfWork.SaveChangesAsync();
        return journalResult;

    }
}
