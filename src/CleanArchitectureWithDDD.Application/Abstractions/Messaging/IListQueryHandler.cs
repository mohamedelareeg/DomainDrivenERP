using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging;
public interface IListQueryHandler<TQuery, TItem> : IRequestHandler<TQuery, Result<CustomList<TItem>>>
       where TQuery : IListQuery<TItem>
{
}
