using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenERP.Domain.Shared.Results;
using MediatR;

namespace DomainDrivenERP.Application.Abstractions.Messaging;
public interface IListQuery<TItem> : IRequest<Result<CustomList<TItem>>>
{

}
