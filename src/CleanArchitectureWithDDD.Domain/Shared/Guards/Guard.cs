using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Guard;

namespace CleanArchitectureWithDDD.Domain.Shared.Guards;
public class Guard : IGuardClause
{
    public static IGuardClause Against { get; } = new Guard();

    private Guard() { }
}
