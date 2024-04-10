using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public class GetCOAByHeadNameSpecification
{
    public static BaseSpecification<COA> GetCOAByHeadNameSpec(string coaParentName)
    {
        var spec = new BaseSpecification<COA>(a => a.HeadName == coaParentName);
        return spec;
    }
}
