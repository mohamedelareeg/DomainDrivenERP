using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Specifications;

namespace CleanArchitectureWithDDD.Persistence.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQueryable,
        BaseSpecification<TEntity> baseSpecification)
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> queryable = inputQueryable;

        if (baseSpecification is not null)
        {
            if (baseSpecification.Criteria is not null)
            {
                queryable = queryable.Where(baseSpecification.Criteria);
            }
            foreach (Expression<Func<TEntity, object>> includeExpression in baseSpecification.IncludeExpressions)
            {
                queryable = queryable.Include(includeExpression);
            }

            if (baseSpecification.OrderByExpression is not null)
            {
                queryable = queryable.OrderBy(baseSpecification.OrderByExpression);
            }
            else if (baseSpecification.OrderByDescendingExpression is not null)
            {
                queryable = queryable.OrderByDescending(baseSpecification.OrderByDescendingExpression);
            }
        }

        return queryable;
    }
}
