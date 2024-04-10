using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public static class IsCoaExistByNameAndParentNameSpecification
{
    public static BaseSpecification<COA> IsCoaExistByNameAndParentNameSpec(string coaName, string coaParentName)
    {
        var spec = new BaseSpecification<COA>(coa => coa.HeadName == coaName && coa.ParentCOA.HeadName == coaParentName);
        spec.AddInclude(coa => coa.ParentCOA);
        return spec;
    }
}
