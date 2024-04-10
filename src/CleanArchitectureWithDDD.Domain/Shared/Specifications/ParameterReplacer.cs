using System.Linq.Expressions;

namespace CleanArchitectureWithDDD.Domain.Shared.Specifications;

internal class ParameterReplacer : ExpressionVisitor
{
    private readonly ParameterExpression _parameter;

    internal ParameterReplacer(ParameterExpression parameter)
    {
        _parameter = parameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return base.VisitParameter(_parameter);
    }
}
