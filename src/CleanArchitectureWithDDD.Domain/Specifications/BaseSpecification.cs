using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureWithDDD.Domain.Primitives;

namespace CleanArchitectureWithDDD.Domain.Specifications;
public class BaseSpecification<T> : ISpecification<T>
{
    public BaseSpecification() { }
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        WhereExpressions.Add(new WhereExpression<T> { Criteria = criteria });
    }

    public List<WhereExpression<T>> WhereExpressions { get; private set; } = new List<WhereExpression<T>>();
    public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
    public List<Expression<Func<T, object>>> ThenIncludes { get; } = new List<Expression<Func<T, object>>>();
    public List<string> IncludeStrings { get; } = new List<string>();
    public List<OrderExpression<T>> OrderByExpressions { get; } = new List<OrderExpression<T>>();
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool isPagingEnabled { get; private set; } = false;

    public virtual void ApplyWhere(Expression<Func<T, bool>> whereExpression)
    {
        WhereExpressions.Add(new WhereExpression<T> { Criteria = whereExpression });
    }

    public virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    public virtual void AddThenInclude(Expression<Func<T, object>> thenIncludeExpression)
    {
        ThenIncludes.Add(thenIncludeExpression);
    }
    public virtual void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }

    public virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        isPagingEnabled = true;
    }

    public virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderByExpressions.Add(new OrderExpression<T> { KeySelector = orderByExpression, OrderType = OrderTypeEnum.OrderBy });
    }

    public virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByExpressions.Add(new OrderExpression<T> { KeySelector = orderByDescendingExpression, OrderType = OrderTypeEnum.OrderByDescending });
    }

    public virtual void ThenBy(Expression<Func<T, object>> thenByExpression)
    {
        OrderByExpressions.Add(new OrderExpression<T> { KeySelector = thenByExpression, OrderType = OrderTypeEnum.ThenBy });
    }

    public virtual void ThenByDescending(Expression<Func<T, object>> thenByDescendingExpression)
    {
        OrderByExpressions.Add(new OrderExpression<T> { KeySelector = thenByDescendingExpression, OrderType = OrderTypeEnum.ThenByDescending });
    }

}
