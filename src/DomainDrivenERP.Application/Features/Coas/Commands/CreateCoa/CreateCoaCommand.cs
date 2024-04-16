using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Entities.COAs;

namespace DomainDrivenERP.Application.Features.Coas.Commands.CreateCoa;
public class CreateCoaCommand : ICommand<COA>
{
    public CreateCoaCommand(string coaName, string coaParentName)
    {
        CoaName = coaName;
        CoaParentName = coaParentName;
    }

    public string CoaName { get; }
    public string CoaParentName { get; }
}
