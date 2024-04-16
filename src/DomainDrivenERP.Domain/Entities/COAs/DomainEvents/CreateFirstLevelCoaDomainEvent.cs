using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.COAs.DomainEvents;
public sealed record CreateFirstLevelCoaDomainEvent(string HeadName, COA_Type Type) : DomainEvent(Guid.NewGuid());
