using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainDrivenERP.Domain.Dtos;
using DomainDrivenERP.Domain.Entities.COAs;

namespace DomainDrivenERP.Application.Features.Coas.Queries.GetCoaWithChildrens;
public class GetCoaWithChildrensQueryMapper : Profile
{
    public GetCoaWithChildrensQueryMapper()
    {
        CreateMap<COA, CoaWithChildrenDto>();
    }
}
