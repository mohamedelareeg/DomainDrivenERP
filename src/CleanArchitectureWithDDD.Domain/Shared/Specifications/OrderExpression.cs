using System.Linq.Expressions;

namespace CleanArchitectureWithDDD.Domain.Shared.Specifications;

public class OrderExpression<T>
{
    public Expression<Func<T, object>> KeySelector { get; set; }
    public OrderTypeEnum OrderType { get; set; }
}
