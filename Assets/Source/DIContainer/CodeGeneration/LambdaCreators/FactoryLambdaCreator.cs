using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EXRContainer.Core;

namespace EXRContainer.CodeGeneration {
    internal class FactoryLambdaCreator : ILambdaCreator<Factory<object>> {
        private readonly LinkedList<IExpressionsProvider> providers;
        private readonly LinkedListNode<IExpressionsProvider> creationNode;

        public FactoryLambdaCreator(ICreationExpressionsProvider provider) {
            providers = new LinkedList<IExpressionsProvider>();
            creationNode = providers.AddFirst(provider);
        }

        public FactoryLambdaCreator(FactoryLambdaCreator other) {
            providers = other.providers;
            creationNode = other.creationNode;
        }

        public FactoryLambdaCreator(ICreationExpressionsProvider provider, FactoryLambdaCreator other) {
            providers = new LinkedList<IExpressionsProvider>(other.providers);
            other.creationNode.Value = provider;
        }

        public void BeforeCreationProvider(IExpressionsProvider provider) 
            => providers.AddBefore(creationNode, provider);

        public void PostCreationProvider(IExpressionsProvider provider) 
            => providers.AddAfter(creationNode, provider);

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