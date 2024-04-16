using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Specifications;

namespace DomainDrivenERP.Domain.Entities.COAs.Specifications;
public class GetCOAByHeadNameSpecification
{
    public static BaseSpecification<COA> GetCOAByHeadNameSpec(string coaParentName)
    {
        var spec = new BaseSpecification<COA>(a => a.HeadName == coaParentName);
        return spec;
    }
}
