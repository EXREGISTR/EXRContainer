using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    public partial class ExpressionsHelper {
        private static readonly MethodInfo resolveMethod = typeof(IDIContext).GetMethod("Resolve")!;

        public static UnaryExpression DescribeResolving(ParameterExpression context, Type dependencyType) {
            var dependencyExpression = Expression.Constant(dependencyType);
            var resolveExecution = Expression.Call(context, resolveMethod, dependencyExpression);
            return Expression.Convert(resolveExecution, dependencyType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BinaryExpression DescribeCast(ParameterExpression to, Type targetType, ParameterExpression from) {
            var convertExpression = Expression.Convert(from, targetType);
            return Expression.Assign(to, convertExpression);
        }
    }
}