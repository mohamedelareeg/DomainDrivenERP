using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Domain.DomainEvents;
public sealed record class JournalCreatedDomainEvent(Guid Id, Guid JournalId, string? Description, DateTime JournalDate) : DomainEvent(Id);
