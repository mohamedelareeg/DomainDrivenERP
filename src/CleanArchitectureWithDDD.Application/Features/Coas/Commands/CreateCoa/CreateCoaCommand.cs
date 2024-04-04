using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Commands.CreateCoa;
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
