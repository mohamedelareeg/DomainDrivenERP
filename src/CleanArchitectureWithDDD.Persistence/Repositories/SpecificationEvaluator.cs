using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Specifications;
using CleanArchitectureWithDDD.Domain.Exceptions;

namespace CleanArchitectureWithDDD.Persistence.Repositories;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        IQueryable<T> query = inputQuery;

        // modify the IQueryable using the specification's criteria expression
        foreach (WhereExpression<T> expression in specification.WhereExpressions)
        {
            query = query.Where(expression.Criteria);
        }

        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,
                                (current, include) => current.Include(include));

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,
                                (current, include) => current.Include(include));

        // Apply ordering if expressions are set
        IOrderedQueryable<T>? orderedQuery = null;
        foreach (OrderExpression<T> orderExpression in specification.OrderByExpressions)
        {
            if (orderExpression.OrderType == OrderTypeEnum.OrderBy)
            {
                orderedQuery = query.OrderBy(orderExpression.KeySelector);
            }
            else if (orderExpression.OrderType == OrderTypeEnum.OrderByDescending)
            {
                orderedQuery = query.OrderByDescending(orderExpression.KeySelector);
            }
            else if (orderedQuery != null && orderExpression.OrderType == OrderTypeEnum.ThenBy)
            {
                orderedQuery = orderedQuery.ThenBy(orderExpression.KeySelector);
            }
            else if (orderedQuery != null && orderExpression.OrderType == OrderTypeEnum.ThenByDescending)
            {
                orderedQuery = orderedQuery.ThenByDescending(orderExpression.KeySelector);
            }
        }

        if (orderedQuery != null)
        {
            query = orderedQuery;
        }

        // Apply paging if enabled
        if (specification.isPagingEnabled)
        {
            query = query.Skip(specification.Skip)
                         .Take(specification.Take);
        }
        return query;
    }
}
