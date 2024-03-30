﻿using CleanArchitectureWithDDD.Domain.Shared;
using MediatR;

namespace CleanArchitectureWithDDD.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
