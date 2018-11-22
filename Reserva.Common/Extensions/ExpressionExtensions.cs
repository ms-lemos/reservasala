using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Reserva.Infra.Extensions
{
    public static class ExpressionHelper
    {
        public static Expression<Func<TTo, bool>> TypeConvert<TFrom, TTo>(
            this Expression<Func<TFrom, bool>> from)
        {
            if (from == null) return null;

            return ConvertImpl<Func<TFrom, bool>, Func<TTo, bool>>(from);
        }

        private static Expression<TTo> ConvertImpl<TFrom, TTo>(Expression<TFrom> from)
            where TFrom : class
            where TTo : class
        {
            // figure out which types are different in the function-signature

            var fromTypes = from.Type.GetGenericArguments();
            var toTypes = typeof(TTo).GetGenericArguments();

            if (fromTypes.Length != toTypes.Length)
                throw new NotSupportedException("Incompatible lambda function-type signatures");

            var typeMap = new Dictionary<Type, Type>();
            for (var i = 0; i < fromTypes.Length; i++)
                if (fromTypes[i] != toTypes[i])
                    typeMap[fromTypes[i]] = toTypes[i];

            // re-map all parameters that involve different types
            var parameterMap = new Dictionary<Expression, Expression>();
            var newParams = GenerateParameterMap(from, typeMap, parameterMap);

            // rebuild the lambda
            var body = new TypeConversionVisitor<TTo>(parameterMap).Visit(from.Body);
            return Expression.Lambda<TTo>(body, newParams);
        }

        private static ParameterExpression[] GenerateParameterMap<TFrom>(
            Expression<TFrom> from,
            Dictionary<Type, Type> typeMap,
            Dictionary<Expression, Expression> parameterMap
        )
            where TFrom : class
        {
            var newParams = new ParameterExpression[from.Parameters.Count];

            for (var i = 0; i < newParams.Length; i++)
            {
                Type newType;
                if (typeMap.TryGetValue(from.Parameters[i].Type, out newType))
                    parameterMap[from.Parameters[i]] =
                        newParams[i] = Expression.Parameter(newType, from.Parameters[i].Name);
            }

            return newParams;
        }


        private class TypeConversionVisitor<T> : ExpressionVisitor
        {
            private readonly Dictionary<Expression, Expression> _parameterMap;

            public TypeConversionVisitor(Dictionary<Expression, Expression> parameterMap)
            {
                _parameterMap = parameterMap;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                // re-map the parameter
                Expression found;
                if (!_parameterMap.TryGetValue(node, out found))
                    found = base.VisitParameter(node);
                return found;
            }

            public override Expression Visit(Expression node)
            {
                var lambda = node as LambdaExpression;
                if (lambda != null && !_parameterMap.ContainsKey(lambda.Parameters.First()))
                    return new TypeConversionVisitor<T>(_parameterMap).Visit(lambda.Body);
                return base.Visit(node);
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                // re-perform any member-binding
                var expr = Visit(node.Expression);
                var exprType = expr.Type;
                if (exprType != node.Type)
                    if (exprType.GetMember(node.Member.Name).Any())
                    {
                        var newMember = exprType.GetMember(node.Member.Name).Single();
                        return Expression.MakeMemberAccess(expr, newMember);
                    }

                return base.VisitMember(node);
            }
        }
    }
}