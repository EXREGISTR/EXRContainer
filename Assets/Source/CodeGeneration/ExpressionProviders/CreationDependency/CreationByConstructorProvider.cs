using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EXRContainer.CodeGeneration.Providers {
    public class CreationByConstructorProvider : IExpressionsProvider {
        public IEnumerable<Expression> GenerateCode(ExecutionContext context) {
            var constructor = context.DependencyType.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Single();
            var resolvingArgumentsExpression = constructor.GetParameters().
                Select(x => ExpressionsHelper.DescribeResolving(context.ContextParameter, x.ParameterType));

            var newExpression = Expression.New(constructor, resolvingArgumentsExpression);
            var instantiateExpression = Expression.Assign(context.TypedInstanceVariable, newExpression);
            yield return instantiateExpression;
        }

        public IEnumerable<ParameterExpression> GetVariables() {
            return Enumerable.Empty<ParameterExpression>();
        }
    }
}