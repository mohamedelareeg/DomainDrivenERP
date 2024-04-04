using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Enums;

namespace CleanArchitectureWithDDD.Domain.DomainEvents;
public sealed record CreateCOADomainEvent(Guid Id, string HeadName, string ParentHeadCode, COA_Type Type) : DomainEvent(Id);
