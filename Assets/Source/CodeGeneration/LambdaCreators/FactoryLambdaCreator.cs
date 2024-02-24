using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    internal class FactoryLambdaCreator : ILambdaCreator<Factory<object>> {
        private readonly LinkedList<IExpressionsProvider> providers;

        public FactoryLambdaCreator() {
            providers = new LinkedList<IExpressionsProvider>();
        }

        public FactoryLambdaCreator(FactoryLambdaCreator other) {
            providers = new LinkedList<IExpressionsProvider>(other.providers);
        }

        public void CreationProvider(IExpressionsProvider provider) {
            if (providers.First != null) {
                providers.First.Value = provider;
                return;
            }

            providers.AddFirst(provider);
        }

        public void WithSuccessor(IExpressionsProvider provider) {
            providers.AddLast(provider);
        }

        public Factory<object> Create(Type dependencyType, LifeTime lifeTime) {
            var instanceVariable = Expression.Variable(dependencyType, $"{dependencyType.Name}");

            var variables = new List<ParameterExpression> { instanceVariable };

            var contextParameter = ExpressionsCache.ContextParameter;
            var expressions = new List<Expression>();

            var context = new ExecutionContext(contextParameter, instanceVariable, dependencyType, lifeTime);

            foreach (var provider in providers) {
                variables.AddRange(provider.GetVariables());
                expressions.AddRange(provider.GenerateCode(context));
            }

            expressions.Add(instanceVariable);
            // generated:
            // return instance;

            var block = Expression.Block(variables, expressions);
            var lambda = Expression.Lambda<Factory<object>>(block, contextParameter);

            return lambda.Compile();
        }
    }
}