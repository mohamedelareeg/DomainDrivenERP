using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging
{
    public interface IQueryHandler<TQuery,TResponse>
        :IRequestHandler<TQuery,Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
