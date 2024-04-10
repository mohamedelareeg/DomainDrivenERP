using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Shared.Results;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging;
public interface IListQuery<TItem> : IRequest<Result<CustomList<TItem>>>
{

}
