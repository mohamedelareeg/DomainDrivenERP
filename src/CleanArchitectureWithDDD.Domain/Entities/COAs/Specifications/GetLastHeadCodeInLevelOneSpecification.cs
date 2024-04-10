using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public static class GetLastHeadCodeInLevelOneSpecification
{
    public static BaseSpecification<COA> GetLastHeadCodeInLevelOneSpec()
    {
        var spec = new BaseSpecification<COA>(coa => coa.HeadLevel == 1);
        spec.ApplyOrderByDescending(coa => coa.HeadCode);
        spec.ApplyPaging(0, 1);
        return spec;
    }
}
