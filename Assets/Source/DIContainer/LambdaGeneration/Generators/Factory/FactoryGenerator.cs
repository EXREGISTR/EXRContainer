using EXRContainer.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    internal static class FactoryGenerator {
        public static Factory<object> Execute(IGenerationExecutor executor, in DependencyGenerationData data) {
            Type dependencyType = data.Type;

            ParameterExpression contextParameter = ExpressionsConstants.ContextParameter;
            ParameterExpression dependencyInstance = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var expressions = new List<Expression>();
            var variables = new List<ParameterExpression> { dependencyInstance };

            GenerationContext context = new GenerationContext(expressions, variables, contextParameter, dependencyInstance, data);

            executor.Execute(context);

            // generated:
            // return dependency;
            context.AppendExpression(context.DependencyInstance);

            return GenerateLambda(expressions, variables, contextParameter);
        }

        private static Factory<object> GenerateLambda(IEnumerable<Expression> expressions, 
            IEnumerable<ParameterExpression> variables, ParameterExpression contextParameter) {
            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Factory<object>>(block, contextParameter);
            return lambda.Compile();
        }
    }
}
