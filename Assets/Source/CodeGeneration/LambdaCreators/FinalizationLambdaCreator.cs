using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    internal class FinalizationLambdaCreator : ILambdaCreator<Finalizator<object>> {
        private readonly LinkedList<IExpressionsProvider> providers;

        public FinalizationLambdaCreator() {
            providers = new LinkedList<IExpressionsProvider>();
        }

        public FinalizationLambdaCreator(FinalizationLambdaCreator other) {
            providers = new LinkedList<IExpressionsProvider>(other.providers);
        }

        public void FirstProvider(IExpressionsProvider provider) => providers.AddFirst(provider);

        public void LastProvider(IExpressionsProvider provider) => providers.AddLast(provider);

        public Finalizator<object> Create(Type dependencyType, LifeTime lifeTime) {
            var typedInstanceVariable = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var variables = new List<ParameterExpression> {
                typedInstanceVariable
            };

            var contextParameter = ExpressionsCache.ContextParameter;
            var instanceParameter = ExpressionsCache.ObjectParameter;

            var castObjectExpression = ExpressionsHelper.DescribeCast(typedInstanceVariable, dependencyType, instanceParameter);
            // SomeType typedInstance = (SomeType)instanceParameter;

            var expressions = new List<Expression>() { castObjectExpression };

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