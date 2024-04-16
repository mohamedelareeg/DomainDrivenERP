using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Enums;
using DomainDrivenERP.Domain.Primitives;

namespace DomainDrivenERP.Domain.Entities.COAs.DomainEvents;
public sealed record CreateCOADomainEvent( string HeadName, string ParentHeadCode, COA_Type Type) : DomainEvent(Guid.NewGuid());
