using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public class GetCOAWithChildrenSpecification
{
    public static BaseSpecification<COA> GetCOAWithChildrenSpec(string coaId)
    {
        var spec = new BaseSpecification<COA>(a => a.HeadCode == coaId);
        spec.AddInclude(c => c.COAs);
        return spec;
    }
}
