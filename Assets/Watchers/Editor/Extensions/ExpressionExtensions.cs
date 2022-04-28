using System;
using System.Linq.Expressions;

namespace Gbros.Watchers
{
    public static class ExpressionExtensions
    {
        public static string GetName<T>(this Expression<T> expression) => expression.Body switch
        {
            MemberExpression memberExpression => memberExpression.Member.Name,
            UnaryExpression unaryExpression when unaryExpression.Operand is MemberExpression memberExpression => memberExpression.Member.Name,
            _ => throw new NotImplementedException(expression.GetType().ToString())
        };
    }
}