using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities;

namespace DomainDrivenERP.Application.Features.Journals.Queries.GetJournalById;
public class GetJournalByIdQuery : IQuery<JournalDto>
{
    public GetJournalByIdQuery(string journalId)
    {
        JournalId = journalId;
    }

    public string JournalId { get; }
}
