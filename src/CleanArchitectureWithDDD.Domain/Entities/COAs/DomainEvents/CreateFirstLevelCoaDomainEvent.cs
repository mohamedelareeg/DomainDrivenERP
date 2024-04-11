using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.DomainEvents;
public sealed record CreateFirstLevelCoaDomainEvent(string HeadName, COA_Type Type) : DomainEvent(Guid.NewGuid());
