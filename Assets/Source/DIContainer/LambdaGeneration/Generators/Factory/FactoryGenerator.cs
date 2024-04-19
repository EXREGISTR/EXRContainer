using EXRContainer.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EXRContainer.LambdaGeneration {
    internal class FactoryGenerator : ILambdaCreator<Factory<object>> {
        private readonly IExpressionProvidersContainer providers;

        public FactoryGenerator(IExpressionProvidersContainer providers) {
            this.providers = providers;
        }

        public Factory<object> Execute(DependencyGenerationData data) {
            Type dependencyType = data.Type;

            ParameterExpression contextParameter = ExpressionsConstants.ContextParameter;
            ParameterExpression dependencyInstance = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var expressions = new List<Expression>();
            var variables = new List<ParameterExpression> { dependencyInstance };

            GenerationContext context = new GenerationContext(expressions, variables, contextParameter, dependencyInstance, data);

            Execute(context);

            return GenerateLambda(expressions, variables, contextParameter);
        }

        private Factory<object> GenerateLambda(IEnumerable<Expression> expressions, 
            IEnumerable<ParameterExpression> variables, ParameterExpression contextParameter) {
            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Factory<object>>(block, contextParameter);
            return lambda.Compile();
        }

        private void Execute(GenerationContext context) {
            providers.ExecuteGeneration(context);

            // generated:
            // return dependency;
            context.AppendExpression(context.DependencyInstance);
        }
    }
}
