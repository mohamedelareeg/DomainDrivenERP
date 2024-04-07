using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.DomainEvents;
public sealed record CreateFirstLevelCoaDomainEvent(Guid Id, string HeadName, COA_Type Type): DomainEvent(Id);
