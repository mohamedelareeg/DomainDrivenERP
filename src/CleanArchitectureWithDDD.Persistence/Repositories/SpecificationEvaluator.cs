using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Domain.Specifications;
using CleanArchitectureWithDDD.Domain.Exceptions;

namespace CleanArchitectureWithDDD.Persistence.Repositories;

public static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQueryable,
        BaseSpecification<TEntity> baseSpecification)
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> queryable = inputQueryable;

        try
        {
            if (baseSpecification == null)
            {
                throw new ArgumentNullException(nameof(baseSpecification), "Base specification cannot be null.");
            }

            if (baseSpecification.Criteria != null)
            {
                queryable = queryable.Where(baseSpecification.Criteria);
            }

            foreach (Expression<Func<TEntity, object>> includeExpression in baseSpecification.IncludeExpressions)
            {
                queryable = queryable.Include(includeExpression);
            }

            if (baseSpecification.OrderByExpression != null)
            {
                queryable = queryable.OrderBy(baseSpecification.OrderByExpression);
            }
            else if (baseSpecification.OrderByDescendingExpression != null)
            {
                queryable = queryable.OrderByDescending(baseSpecification.OrderByDescendingExpression);
            }

            return queryable;
        }
        catch (ArgumentNullException ex)
        {
            throw new SpecificationEvaluationException("Base specification cannot be null.", ex);
        }
        catch (Exception ex)
        {
            throw new SpecificationEvaluationException("Error occurred while evaluating specification.", ex);
        }
    }
}
