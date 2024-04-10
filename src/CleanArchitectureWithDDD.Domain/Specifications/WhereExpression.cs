using System.Linq.Expressions;

namespace CleanArchitectureWithDDD.Domain.Specifications;

public class WhereExpression<T>
{
    public Expression<Func<T, bool>> Criteria { get; set; }
}
