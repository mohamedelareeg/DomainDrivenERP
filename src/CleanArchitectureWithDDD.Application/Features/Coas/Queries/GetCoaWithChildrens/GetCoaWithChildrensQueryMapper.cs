using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaWithChildrens;
public class GetCoaWithChildrensQueryMapper : Profile
{
    public GetCoaWithChildrensQueryMapper()
    {
        CreateMap<COA, CoaWithChildrenDto>();
    }
}
