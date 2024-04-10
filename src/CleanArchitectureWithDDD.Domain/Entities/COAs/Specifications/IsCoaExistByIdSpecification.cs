using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public static class IsCoaExistByIdSpecification
{
    public static BaseSpecification<COA> IsCoaExistByIdSpec(string coaId)
    {
        var spec = new BaseSpecification<COA>(a => a.HeadCode == coaId);
        return spec;
    }
}
