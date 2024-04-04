using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Dtos;
using CleanArchitectureWithDDD.Domain.Entities;

namespace CleanArchitectureWithDDD.Application.Features.Coas.Queries.GetCoaWithChildrens;
public class GetCoaWithChildrensQuery : IQuery<CoaWithChildrenDto>
{
    public GetCoaWithChildrensQuery(string headCode)
    {
        HeadCode = headCode;
    }

    public string HeadCode { get; }
}
