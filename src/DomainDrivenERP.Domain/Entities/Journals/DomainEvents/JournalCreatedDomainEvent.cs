using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.Journals.DomainEvents;
public sealed record class JournalCreatedDomainEvent(Guid JournalId, string? Description, DateTime JournalDate) : DomainEvent(Guid.NewGuid());
