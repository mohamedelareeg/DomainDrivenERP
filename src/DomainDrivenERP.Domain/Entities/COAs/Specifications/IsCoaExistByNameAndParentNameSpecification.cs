using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.COAs.Specifications;
public static class IsCoaExistByNameAndParentNameSpecification
{
    public static BaseSpecification<COA> IsCoaExistByNameAndParentNameSpec(string coaName, string coaParentName)
    {
        var spec = new BaseSpecification<COA>(coa => coa.HeadName == coaName && coa.ParentCOA.HeadName == coaParentName);
        spec.AddInclude(coa => coa.ParentCOA);
        return spec;
    }
}
