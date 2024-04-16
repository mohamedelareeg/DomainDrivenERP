using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.COAs.Specifications;
public static class GetCOAByHeadCodeSpecification
{
    public static BaseSpecification<COA> GetCOAByHeadCodeSpec(string headCode)
    {
        var spec = new BaseSpecification<COA>(a => a.HeadCode == headCode);
        return spec;
    }
}
