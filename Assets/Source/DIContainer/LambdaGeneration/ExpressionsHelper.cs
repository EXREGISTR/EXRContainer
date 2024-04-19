using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EXRContainer.Core;

namespace EXRContainer.LambdaGeneration {
    public partial class ExpressionsHelper {
        private static readonly MethodInfo addDependencyMethod = typeof(IDIContext).GetMethod("AddDependency");
        private static readonly MethodInfo resolveMethod = typeof(IDIContext).GetMethod("Resolve")!;

        public static MethodCallExpression DescribeAddDependencyToContext(
            ParameterExpression context, ParameterExpression variable) {
            var method = addDependencyMethod.MakeGenericMethod(variable.Type);
            return Expression.Call(context, method, variable);
        }

        public static UnaryExpression DescribeResolving(ParameterExpression context, Type dependencyType) {
            var dependencyExpression = Expression.Constant(dependencyType);
            var resolveExecution = Expression.Call(context, resolveMethod, dependencyExpression);
            return Expression.Convert(resolveExecution, dependencyType);
        }

        public static IEnumerable<UnaryExpression> DescribeResolving(
            ParameterExpression context,
            IEnumerable<ParameterInfo> arguments) {
            return arguments.Select(x => DescribeResolving(context, x.ParameterType));
        }

        public static BinaryExpression DescribeCast(ParameterExpression to, Type targetType, ParameterExpression from) {
            var convertExpression = Expression.Convert(from, targetType);
            return Expression.Assign(to, convertExpression);
        }
    }
}