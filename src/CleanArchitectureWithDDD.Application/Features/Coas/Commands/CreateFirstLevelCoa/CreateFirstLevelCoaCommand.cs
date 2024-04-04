using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Entities;
using CleanArchitectureWithDDD.Domain.Enums;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Commands.CreateFirstLevelCoa;
public class CreateFirstLevelCoaCommand : ICommand<COA>
{
    public CreateFirstLevelCoaCommand(string headName, COA_Type type)
    {
        HeadName = headName;
        Type = type;
    }

    public string HeadName { get; }
    public COA_Type Type { get; }
}
