using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Application.Features.Journals.Queries.GetJournalById;
public class GetJournalByIdQuery : IQuery<JournalDto>
{
    public GetJournalByIdQuery(string journalId)
    {
        JournalId = journalId;
    }

    public string JournalId { get; }
}
