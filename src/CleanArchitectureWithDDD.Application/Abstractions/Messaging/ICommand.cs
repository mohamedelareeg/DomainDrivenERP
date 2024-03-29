using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>
    {

    }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {

    }
}
