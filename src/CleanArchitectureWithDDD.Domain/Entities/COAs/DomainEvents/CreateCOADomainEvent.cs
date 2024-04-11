using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Enums;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.DomainEvents;
public sealed record CreateCOADomainEvent( string HeadName, string ParentHeadCode, COA_Type Type) : DomainEvent(Guid.NewGuid());
