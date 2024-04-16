using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Application.Abstractions.Messaging;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaWithChildrens;
public class GetCoaWithChildrensQuery : IQuery<CoaWithChildrenDto>
{
    public GetCoaWithChildrensQuery(string headCode)
    {
        HeadCode = headCode;
    }

    public string HeadCode { get; }
}
