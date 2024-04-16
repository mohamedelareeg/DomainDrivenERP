using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenERP.Domain.Shared.Specifications;
public interface ISpecification<T>
{
    List<WhereExpression<T>> WhereExpressions { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<string> IncludeStrings { get; }
    List<OrderExpression<T>> OrderByExpressions { get; }

    int Take { get; }
    int Skip { get; }
    bool isPagingEnabled { get; }
}
