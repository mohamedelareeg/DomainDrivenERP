using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public static class IsCoaExistByNameAndLevelSpecification
{
    public static BaseSpecification<COA> IsCoaExistByNameAndLevelSpec(string coaName, int level = 1)
    {
        var spec = new BaseSpecification<COA>(coa => coa.HeadName == coaName && coa.HeadLevel == level);
        return spec;
    }
}
