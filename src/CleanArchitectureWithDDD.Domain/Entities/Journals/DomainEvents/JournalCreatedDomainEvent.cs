using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.Journals.DomainEvents;
public sealed record class JournalCreatedDomainEvent(Guid JournalId, string? Description, DateTime JournalDate) : DomainEvent(Guid.NewGuid());
