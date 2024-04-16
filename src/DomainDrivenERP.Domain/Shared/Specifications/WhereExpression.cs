using System.Linq.Expressions;

namespace DomainDrivenERP.Domain.Shared.Specifications;

public class WhereExpression<T>
{
    public Expression<Func<T, bool>> Criteria { get; set; }
}
