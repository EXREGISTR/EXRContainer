using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    internal class FinalizationLambdaCreator {
        private readonly IEnumerable<IExpressionsProvider> providers;

        public FinalizationLambdaCreator(IEnumerable<IExpressionsProvider> providers) {
            this.providers = providers;
        }

        public Finalizator<object> Create(Type dependencyType, LifeTime lifeTime) {
            var typedInstanceVariable = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var variables = new List<ParameterExpression> {
                typedInstanceVariable
            };

            var contextParameter = ExpressionsCache.ContextParameter;
            var instanceParameter = ExpressionsCache.ObjectParameter;

            var describedCastExpression = ExpressionsHelper.DescribeCast(typedInstanceVariable, dependencyType, instanceParameter);

            var expressions = new List<Expression>() { describedCastExpression };

            var context = new ExecutionContext(contextParameter, typedInstanceVariable, dependencyType, lifeTime);
            foreach (var provider in providers) {
                variables.AddRange(provider.GetVariables());
                expressions.AddRange(provider.GenerateCode(context));
            }

            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Finalizator<object>>(block, contextParameter, instanceParameter);
            return lambda.Compile();
        }
    }
}