using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    internal static class LambdaGenerator<TDelegate> {
        public static TDelegate Execute(IGenerationExecutor executor, in DependencyGenerationData data) {
            var dependencyType = data.Type;
            var typedInstanceVariable = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var contextParameter = ExpressionsConstants.ContextParameter;
            var instanceParameter = ExpressionsConstants.ObjectParameter;

            var castObjectExpression = ExpressionsHelper.DescribeCast(typedInstanceVariable, dependencyType, instanceParameter);
            // generated:
            // SomeType typedInstance = (SomeType)instanceParameter;

            var expressions = new List<Expression> { castObjectExpression };
            var variables = new List<ParameterExpression> { typedInstanceVariable };

            var context = new GenerationContext(expressions, variables, contextParameter, typedInstanceVariable, data);

            executor.Execute(context);

            return GenerateLambda(expressions, variables, contextParameter, instanceParameter);
        }

        private static TDelegate GenerateLambda(IEnumerable<Expression> expressions,
            IEnumerable<ParameterExpression> variables, ParameterExpression contextParameter,
            ParameterExpression instanceParameter) {
            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<TDelegate>(block, contextParameter, instanceParameter);
            return lambda.Compile();
        }
    }
}