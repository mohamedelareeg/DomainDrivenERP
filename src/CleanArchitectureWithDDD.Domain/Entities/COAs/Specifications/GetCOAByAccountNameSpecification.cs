using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Domain.Entities.COAs.Specifications;
public static class GetCOAByAccountNameSpecification
{
    public static BaseSpecification<COA> GetCOAByAccountNameSpec(string headName)
    {
        var spec = new BaseSpecification<COA>(a => a.HeadName == headName);
        return spec;
    }
}
