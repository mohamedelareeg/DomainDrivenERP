using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public class GetCOAChildsSpecification
{
    public static BaseSpecification<COA> GetCOAChildsSpec(string parentCoaId)
    {
        var spec = new BaseSpecification<COA>(a => a.ParentHeadCode == parentCoaId);
        return spec;
    }
}
