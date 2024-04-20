using System.Collections.Generic;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.LambdaGeneration {
    internal class FinalizatorGenerator : ILambdaCreator<Finalizator<object>> {
        private readonly IGenerationExecutor executor;

        public FinalizatorGenerator(IGenerationExecutor providers) {
            this.executor = providers;
        }

        public Finalizator<object> Execute(DependencyGenerationData data) {
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

        private Finalizator<object> GenerateLambda(IEnumerable<Expression> expressions,
            IEnumerable<ParameterExpression> variables, ParameterExpression contextParameter,
            ParameterExpression instanceParameter) {
            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Finalizator<object>>(block, contextParameter, instanceParameter);
            return lambda.Compile();
        }
    }
}